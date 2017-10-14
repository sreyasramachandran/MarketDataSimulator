using MarketData.Common;
using MarketData.Common.Interfaces;
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
    [Export(typeof(IReferenceData))]
    public class ReferenceDataSimulator : IReferenceData
    {
        private List<ISecurityInfo> _securities;

        public IEnumerable<ISecurityInfo> GetSecurities()
        {
            if (null == _securities)
	        {
                var serializer = new XmlSerializer(typeof(Security[]));
                using (var stream = File.OpenRead("ReferenceDataDB.xml"))
                {
                    var securities = (Security[])serializer.Deserialize(stream);
                    _securities = new List<ISecurityInfo>();

                    foreach (var security in securities)
                    {
                        _securities.Add(security);
                    }
                }
	        }

            return _securities;
        }
    }
}
