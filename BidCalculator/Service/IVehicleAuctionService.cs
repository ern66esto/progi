using BidCalculator.Dtos;

namespace BidCalculator.Service
{
    public interface IVehicleAuctionService
    {
        public Task<BidCalculatorDto> CalculateTotalPrice(double basePrice, string type);
    }
}
