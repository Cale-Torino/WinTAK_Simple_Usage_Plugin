//using Prism.Events;
//using Prism.Mef.Modularity;
//using Prism.Modularity;
using System;
using System.Collections.Generic;
//using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinTAK_Simple_Usage_Plugin
{
    [Prism.Mef.Modularity.ModuleExport(typeof(Plugin_Module), InitializationMode = Prism.Modularity.InitializationMode.WhenAvailable)]
    internal class Plugin_Module : Prism.Modularity.IModule
    {
        private readonly Prism.Events.IEventAggregator _eventAggregator;

        [System.ComponentModel.Composition.ImportingConstructor]
        public Plugin_Module(Prism.Events.IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }
        // Modules will be initialized during startup. Any work that needs to be done at startup can
        // be initiated from here.
        public void Initialize()
        {

        }
    }
}
