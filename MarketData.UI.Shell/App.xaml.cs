using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace MarketData.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var directory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            DirectoryCatalog dllCatalog = new DirectoryCatalog(directory, "MarketData.*.dll");
            DirectoryCatalog exeCatalog = new DirectoryCatalog(directory, "MarketData.*.exe");
            AggregateCatalog catalog = new AggregateCatalog(dllCatalog, exeCatalog);
            var container = new CompositionContainer(catalog);
            var batch = new CompositionBatch();
            batch.AddPart(this);
            container.Compose(batch);
           
            this.MainWindow = TheMainWindow;
            this.MainWindow.Show();
        }

        [Import(typeof(Window))]
        public Window TheMainWindow { get; set; }
    }
}
