using System;
using System.Collections;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace Momo.AutoUpgrade
{
    /// <summary>   
    /// 实现类型属性分别编辑的通用类型转换器   
    /// 使用注意：使用的时候应该先从这个通用类型转换器中继承一个自己的类型转换器，泛型T应该是类型转换器的目标类型   
    /// </summary>   
    /// <typeparam name="T"></typeparam>   
    public class TypeConverterGeneral<T> : TypeConverter where T : new()
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return ((sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType));
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            //字符串类似：ClassName { A=a, B=b, C=c }   
            string strValue = value as string;
            if (strValue == null)
            {
                return base.ConvertFrom(context, culture, value);
            }
            strValue = strValue.Trim();
            if (strValue.Length == 0)
            {
                return null;
            }
            if (culture == null)
            {
                culture = CultureInfo.CurrentCulture;
            }
            //char sepChar = culture.TextInfo.ListSeparator[0];   
            char sepChar = '|';

            Type type = typeof(T);
            //1、去掉“ClassName { ”和“ }”两部分   
            string withStart = type.Name + " { ";
            string withEnd = " }";
            if (strValue.StartsWith(withStart) && strValue.EndsWith(withEnd))
            {
                strValue = strValue.Substring(withStart.Length, strValue.Length - withStart.Length - withEnd.Length);
            }
            //2、分割属性值   
            string[] strArray = strValue.Split(new char[] { sepChar });
            //3、做成属性集合表   
            Hashtable properties = new Hashtable();
            for (int i = 0; i < strArray.Length; i++)
            {
                string str = strArray[i].Trim();
                int index = str.IndexOf('=');
                if (index != -1)
                {
                    string propName = str.Substring(0, index);
                    string propValue = str.Substring(index + 1, str.Length - index - 1);
                    PropertyInfo pi = type.GetProperty(propName);
                    if (pi != null)
                    {
                        //该属性对应类型的类型转换器   
                        TypeConverter converter = TypeDescriptor.GetConverter(pi.PropertyType);
                        properties.Add(propName, converter.ConvertFromString(propValue));
                    }
                }
            }
            return this.CreateInstance(context, properties);
        }
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string)) return true;
            if (destinationType == typeof(InstanceDescriptor)) return true;

            return base.CanConvertTo(context, destinationType);
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }
            if (value is T)
            {
                if (destinationType == typeof(string))
                {
                    if (culture == null)
                    {
                        culture = CultureInfo.CurrentCulture;
                    }
                    //string separator = culture.TextInfo.ListSeparator + " ";   
                    string separator = " | ";
                    StringBuilder sb = new StringBuilder();
                    Type type = value.GetType();
                    sb.Append(type.Name + " { ");
                    PropertyInfo[] pis = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                    for (int i = 0; i < pis.Length; i++)
                    {
                        if (!pis[i].CanRead) continue;

                        Type typeProp = pis[i].PropertyType;
                        string nameProp = pis[i].Name;
                        object valueProp = pis[i].GetValue(value, null);

                        TypeConverter converter = TypeDescriptor.GetConverter(typeProp);

                        sb.AppendFormat("{0}={1}" + separator, nameProp, converter.ConvertToString(context, valueProp));
                    }
                    string strContent = sb.ToString();
                    if (strContent.EndsWith(separator))
                        strContent = strContent.Substring(0, strContent.Length - separator.Length);
                    strContent += " }";
                    return strContent;
                }
                if (destinationType == typeof(InstanceDescriptor))
                {
                    ConstructorInfo constructor = typeof(T).GetConstructor(new Type[0]);
                    if (constructor != null)
                    {
                        return new InstanceDescriptor(constructor, new object[0], false);
                    }
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            if (propertyValues == null)
            {
                throw new ArgumentNullException("propertyValues");
            }
            Type type = typeof(T);
            ConstructorInfo ci = type.GetConstructor(new Type[0]);
            if (ci == null) return null;
            //调用默认的构造函数构造实例   
            object obj = ci.Invoke(new object[0]);
            //设置属性   
            PropertyInfo[] pis = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            object propValue = null;
            for (int i = 0; i < pis.Length; i++)
            {
                if (!pis[i].CanWrite) continue;
                propValue = propertyValues[pis[i].Name];
                if (propValue != null)
                    pis[i].SetValue(obj, propValue, null);
            }
            return obj;
        }

        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return true;//返回更改此对象的值是否要求调用 CreateInstance 方法来创建新值。   
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            //属性依照在类型中声明的顺序显示   
            Type type = value.GetType();
            PropertyInfo[] pis = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            string[] names = new string[pis.Length];
            for (int i = 0; i < names.Length; i++)
            {
                names[i] = pis[i].Name;
            }
            return TypeDescriptor.GetProperties(typeof(T), attributes).Sort(names);
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}
