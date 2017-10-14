using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketData.Common.Interfaces
{
    public interface IReferenceData
    {
        IEnumerable<ISecurityInfo> GetSecurities();
    }
}
