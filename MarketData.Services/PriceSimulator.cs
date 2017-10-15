using MarketData.Common.Interfaces;
using MarketData.Simulator;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Xml.Serialization;

namespace MarketData.Services
{
    [Export(typeof(IPriceSource))]
    [Export(typeof(IPricePublisher))]
    public sealed class PriceSimulator: IPriceSource, IPricePublisher, IDisposable
    {
        private Dictionary<int, decimal> _prices = new Dictionary<int, decimal>();
        
        private Timer _tick;
        Random _stockMovement = new Random();

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

            _tick = new Timer(5000);
            _tick.Elapsed += _tick_Elapsed;
            _tick.Start();
        }

        public decimal GetPrice(int securityID)
        {
            if (!_prices.Keys.Contains(securityID))
            {
                throw new Exception("Unknown security / Price not available");
            }

            return _prices[securityID];
        }

        public event PriceUpdated PriceUpdated;

        void _tick_Elapsed(object sender, ElapsedEventArgs e)
        {
            _tick.Stop();
            foreach (var securityID in _prices.Keys.ToList())
            {
                decimal change = (decimal)1 + _stockMovement.Next(-30, 30) * (decimal).0001;
                _prices[securityID] = _prices[securityID] * change;

                if (null != PriceUpdated)
                {
                    PriceUpdated.Invoke(securityID, _prices[securityID]);
                }
            }
            _tick.Start();
        }

        public void Dispose()
        {
            _tick.Stop();
            _tick.Dispose();
        }
    }
}
