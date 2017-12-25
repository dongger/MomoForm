using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Momo.Forms
{
    internal class MTabControlDesigner : ParentControlDesigner
    {
        #region Variables
        /// <summary>
        /// 
        /// </summary>
        private readonly DesignerVerbCollection _verbs = new DesignerVerbCollection();
        /// <summary>
        /// 
        /// </summary>
        private IDesignerHost _designerHost;
        /// <summary>
        /// 
        /// </summary>
        private ISelectionService _selectionService;
        #endregion

        #region Fields
        public override SelectionRules SelectionRules
        {
            get
            {
                return Control.Dock == DockStyle.Fill ? SelectionRules.Visible : base.SelectionRules;
            }
        }
        public override DesignerVerbCollection Verbs
        {
            get
            {
                if (_verbs.Count == 2)
                {
                    var myControl = (MTabControl)Control;
                    _verbs[1].Enabled = myControl.Pages.Count != 0;
                }
                return _verbs;
            }
        }

        public IDesignerHost DesignerHost
        {
            get
            {
                return _designerHost ?? (_designerHost = (IDesignerHost)(GetService(typeof(IDesignerHost))));
            }
        }

        public ISelectionService SelectionService
        {
            get
            {
                return _selectionService ?? (_selectionService = (ISelectionService)(GetService(typeof(ISelectionService))));
            }
        }
        #endregion

        #region Constructor
        public MTabControlDesigner()
        {
            var verb1 = new DesignerVerb("添加选项卡", OnAddPage);
            var verb2 = new DesignerVerb("移除选项卡", OnRemovePage);
            _verbs.AddRange(new[] { verb1, verb2 });
        }
        #endregion

        #region Private methods
        private void OnAddPage(Object sender, EventArgs e)
        {
            var parentControl = (MTabControl)Control;
            var oldTabs = parentControl.Pages;
            RaiseComponentChanging(TypeDescriptor.GetProperties(parentControl)["Pages"]);

            var p = (DTabPage)(DesignerHost.CreateComponent(typeof(DTabPage)));
            p.Text = "选项卡";

            parentControl.Pages.Add(p);

            RaiseComponentChanged(TypeDescriptor.GetProperties(parentControl)["Pages"],
                                  oldTabs, parentControl.Pages);
            parentControl.SelectedIndex = p.Index;

        }

        private void OnRemovePage(Object sender, EventArgs e)
        {
            var parentControl = (MTabControl)Control;
            var oldTabs = parentControl.Pages;
            
            if (parentControl.SelectedIndex < 0)
            {
                return;
            }

            var control = parentControl.Pages[parentControl.SelectedIndex];

            RaiseComponentChanging(TypeDescriptor.GetProperties(parentControl)["Controls"]);

            DesignerHost.DestroyComponent(control);

            RaiseComponentChanged(TypeDescriptor.GetProperties(parentControl)["Controls"],
                                  oldTabs, parentControl.Controls);

            SelectionService.SetSelectedComponents(new IComponent[]
            {
                parentControl
            }, SelectionTypes.Auto);

        }

        #endregion

        #region Overrides
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            switch (m.Msg)
            {
                case WM.WM_NCHITTEST:
                    if (m.Result.ToInt32() == (int)HITTEST.HTTRANSPARENT)
                    {
                        m.Result = (IntPtr)HITTEST.HTCLIENT;
                    }
                    break;
            }
        }

        protected override bool GetHitTest(System.Drawing.Point point)
        {
            if (SelectionService.PrimarySelection == Control)
            {
                var hti = new TCHITTESTINFO
                {
                    Point = Control.PointToClient(point),
                    Flags = 0
                };

                var m = new Message
                {
                    HWnd = Control.Handle,
                    Msg = TCM.TCM_HITTEST
                };

                var lparam =
                    System.Runtime.InteropServices.Marshal.AllocHGlobal(System.Runtime.InteropServices.Marshal.SizeOf(hti));
                System.Runtime.InteropServices.Marshal.StructureToPtr(hti,
                                                                      lparam, false);
                m.LParam = lparam;

                base.WndProc(ref m);
                System.Runtime.InteropServices.Marshal.FreeHGlobal(lparam);

                if (m.Result.ToInt32() != -1)
                {
                    return hti.Flags != (int)TCHT.TCHT_NOWHERE;
                }
            }

            return false;
        }

        protected override void PreFilterProperties(IDictionary properties)
        {
            properties.Remove("UseVisualStyleBackColor");
            base.PreFilterProperties(properties);
        }
        #endregion
    }
}