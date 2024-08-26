using BidCalculator.Dtos;
using BidCalculator.Gateway;

namespace BidCalculator.Service
{
    public class VehicleAuctionService : IVehicleAuctionService
    {
        private readonly IVehicleAuctionGateway _vehicleAuctionGateway;
        public VehicleAuctionService(IVehicleAuctionGateway vehicleAuctionGateway) {
            _vehicleAuctionGateway = vehicleAuctionGateway;
        }

        public async Task<BidCalculatorDto> CalculateTotalPrice(double basePrice, string type)
        {
            BidCalculatorDto bidCalculatorDto = await _vehicleAuctionGateway.CalculateTotalPrice(basePrice, type);
            return bidCalculatorDto;
        }
    }
}
