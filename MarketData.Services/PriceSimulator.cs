using MarketData.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketData.Services
{
    [Export(typeof(IPriceSource))]
    [Export(typeof(IPricePublisher))]
    public class PriceSimulator: IPriceSource, IPricePublisher
    {
        public decimal GetPrice(int SecurityID)
        {
            return 1;
        }

        public event PriceUpdated PriceUpdated;
    }
}
