using MarketData.Common.Interfaces;
using MarketData.Simulator;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MarketData.Services
{
    [Export(typeof(IPriceSource))]
    [Export(typeof(IPricePublisher))]
    public class PriceSimulator: IPriceSource, IPricePublisher
    {
        private Dictionary<int, decimal> _prices = new Dictionary<int, decimal>();

        public PriceSimulator()
        {
            var serializer = new XmlSerializer(typeof(PersistedPriceData[]));
            using (var stream = File.OpenRead("PriceSimulator_SeedValues.xml"))
            {
                var prices = (PersistedPriceData[])serializer.Deserialize(stream);

                foreach (var price in prices)
                {
                    _prices.Add(price.SecurityID, price.Price);
                }
            }
        }

        public decimal GetPrice(int securityID)
        {
            if (!_prices.Keys.Contains(securityID))
            {
                throw new Exception("Unknown security");
            }

            return _prices[securityID];
        }

        public event PriceUpdated PriceUpdated;
    }
}
