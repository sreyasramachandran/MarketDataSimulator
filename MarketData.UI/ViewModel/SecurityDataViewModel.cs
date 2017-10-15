using MarketData.Common.Interfaces;
using MarketData.UI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketData.UI.ViewModel
{
    public class SecurityDataViewModel : NotificationBase
    {
        private ISecurityInfo _security;
        public ISecurityInfo Security
        {
            get
            {
                return _security;
            }
            private set
            {
                _security = value;
                base.NotifyPropertyChanged();
            }
        }

        private decimal? _prevPrice;
        public decimal? PrevPrice
        {
            get
            {
                return _prevPrice;
            }
            set
            {
                _prevPrice = value;
                base.NotifyPropertyChanged();
            }
        }

        private decimal? _price;
        public decimal? Price
        {
            get 
            {
                return _price;
            }
            set 
            {
                if (value == _price)
                {
                    return;
                }

                if (_price.HasValue)
                {
                    PrevPrice = _price;
                }
                _price = value;
                base.NotifyPropertyChanged();
            }
        }

        public SecurityDataViewModel(ISecurityInfo security)
        {
            Security = security;
        }
    }
}
