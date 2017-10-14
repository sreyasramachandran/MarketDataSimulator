using System;
namespace MarketData.Common.Interfaces
{
    public delegate void PriceUpdated(int securityID, decimal price);

    public interface IPricePublisher
    {
        event PriceUpdated PriceUpdated;
    }
}
