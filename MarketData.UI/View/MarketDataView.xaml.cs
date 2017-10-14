using MarketData.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MarketData.UI.View
{
    /// <summary>
    /// Interaction logic for MarketDataView.xaml
    /// </summary>
    [Export("RootView", typeof(UserControl))]
    public partial class MarketDataView : UserControl
    {
        [ImportingConstructor]
        public MarketDataView(MarketDataViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
