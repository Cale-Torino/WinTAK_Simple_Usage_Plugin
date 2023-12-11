using System;
using System.Collections.Generic;
//using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using WinTak.Framework.Docking;
//using WinTak.Framework.Tools;
//using WinTak.Framework.Tools.Attributes;


namespace WinTAK_Simple_Usage_Plugin
{
    [WinTak.Framework.Tools.Attributes.Button("Example_Plugin_Button", "Simple Plugin",
    LargeImage = "pack://application:,,,/WinTAK_Simple_Usage_Plugin;component/Img/tech.png",
    SmallImage = "pack://application:,,,/WinTAK_Simple_Usage_Plugin;component/Img/tech.ico")]

    internal class Plugin_Button : WinTak.Framework.Tools.Button
    {
        private WinTak.Framework.Docking.IDockingManager _dockingManager;

        [System.ComponentModel.Composition.ImportingConstructor]
        public Plugin_Button(WinTak.Framework.Docking.IDockingManager dockingManager)
        {
            _dockingManager = dockingManager;
        }

        protected override void OnClick()
        {
            base.OnClick();

            var pane = _dockingManager.GetDockPane(Plugin_DockPane.ID);
            pane?.Activate();
        }
    }
}
