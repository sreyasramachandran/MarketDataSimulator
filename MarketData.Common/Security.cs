using MarketData.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketData.Common
{
    [Serializable]
    public class Security : ISecurityInfo
    {
        public int SecurityID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Ticker
        {
            get;
            set;
        }
    }
}
