using MarketData.Common.Interfaces;
using MarketData.UI.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketData.UI.ViewModel
{
    [Export(typeof(MarketDataViewModel))]
    public class MarketDataViewModel : NotificationBase
    {
        private IPricePublisher _pricePublisher;
        private IPriceSource _priceSource;
        private IReferenceData _referenceData;

        private readonly ObservableCollection<SecurityDataViewModel> _securityPrices = new ObservableCollection<SecurityDataViewModel>();
        public ObservableCollection<SecurityDataViewModel> SecurityPrices
        {
            get
            {
                return _securityPrices;
            }
        }

        [ImportingConstructor]
        public MarketDataViewModel(IPricePublisher pricePublisher, IPriceSource priceSource, IReferenceData referenceData)
        {
            _pricePublisher = pricePublisher;
            _priceSource = priceSource;
            _referenceData = referenceData;

            foreach (var security in _referenceData.GetSecurities())
	        {
                _securityPrices.Add(new SecurityDataViewModel(security));
	        }

            RefreshPrices();
        }

        private void RefreshPrices()
        {
            foreach (var secVM in SecurityPrices)
            {
                secVM.Price = _priceSource.GetPrice(secVM.Security.SecurityID);
            }
        }
    }
}
