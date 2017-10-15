using MarketData.Common.Interfaces;
using MarketData.UI.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace MarketData.UI.ViewModel
{
    [Export(typeof(MarketDataViewModel))]
    public class MarketDataViewModel : NotificationBase
    {
        private Dispatcher _dispatcher;

        private IPricePublisher _pricePublisher;
        private IPriceSource _priceSource;
        private IReferenceData _referenceData;

        #region Bound Properties
        
        private readonly ObservableCollection<SecurityDataViewModel> _securityPrices = new ObservableCollection<SecurityDataViewModel>();
        public ObservableCollection<SecurityDataViewModel> SecurityPrices
        {
            get
            {
                return _securityPrices;
            }
        }

        private bool _livePrices;
        public bool LivePrices
        {
            get
            {
                return _livePrices;
            }
            set
            {
                _livePrices = value;
                base.NotifyPropertyChanged();
            }
        }

        private string _filterText = string.Empty;
        public string FilterText
        {
            get
            {
                return _filterText;
            }
            set
            {
                _filterText = value;
                base.NotifyPropertyChanged();
            }
        } 
        #endregion

        #region Commands
        public ICommand RefreshPricesCmd
        {
            get;
            private set;
        }

        public ICommand LivePricesCmd
        {
            get;
            private set;
        }

        public Predicate<object> Filter
        {
            get;
            private set;
        }
        #endregion

        [ImportingConstructor]
        public MarketDataViewModel(IPricePublisher pricePublisher, IPriceSource priceSource, IReferenceData referenceData)
        {
            _dispatcher = Dispatcher.CurrentDispatcher;
            _pricePublisher = pricePublisher;
            _priceSource = priceSource;
            _referenceData = referenceData;

            Filter = FilterRow;

            RefreshPricesCmd = new DelegateCommand((object param) =>  RefreshPrices());
            LivePricesCmd = new DelegateCommand((object param) => ConfigureLivePriceUpdates());

            foreach (var security in _referenceData.GetSecurities())
	        {
                _securityPrices.Add(new SecurityDataViewModel(security));
	        }

            RefreshPrices();
        }

        void _pricePublisher_PriceUpdated(int securityID, decimal price)
        {
            _dispatcher.BeginInvoke((Action)(() =>
                                    {
                                        var viewModel = SecurityPrices.FirstOrDefault(x => x.Security.SecurityID == securityID);
                                        viewModel.Price = price;
                                    }));
        }

        private void RefreshPrices()
        {
            foreach (var secVM in SecurityPrices)
            {
                secVM.Price = _priceSource.GetPrice(secVM.Security.SecurityID);
            }
        }

        private void ConfigureLivePriceUpdates()
        {
            if (LivePrices)
            {
                RefreshPrices();
                _pricePublisher.PriceUpdated += _pricePublisher_PriceUpdated;
            }
            else
            {
                _pricePublisher.PriceUpdated -= _pricePublisher_PriceUpdated;
            }
        }

        private bool FilterRow(object param)
        {
            if (string.IsNullOrEmpty(FilterText))
            {
                return true;
            }
            var itemViewModel = param as SecurityDataViewModel;
            
            if (itemViewModel == null)
            {
                return false;
            }

            return itemViewModel.Security.Name.ToLower().Contains(FilterText.Trim().ToLower());
        }
    }
}
