using BidCalculator.Dtos;

namespace BidCalculator.Gateway
{
    public interface IVehicleAuctionGateway
    {
        public Task<BidCalculatorDto> CalculateTotalPrice(double basePrice, string type);
    }
}
