using System;
namespace MarketData.Common.Interfaces
{
    public interface ISecurityInfo
    {
        string Name { get; }
        int SecurityID { get; }
        string Ticker { get; }
    }
}
