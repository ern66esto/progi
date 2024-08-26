using Xunit;
using FakeItEasy;
using BidCalculator.Service;
using BidCalculator.Gateway;
using Moq;
using BidCalculator.Dtos;

namespace BidCalculatorUnitTest
{
    public class VehicleAuctionServiceTesst
    {
        private readonly IVehicleAuctionService _vehicleAuctionService;

        public VehicleAuctionServiceTesst() 
        {
            _vehicleAuctionService = A.Fake<IVehicleAuctionService>();
        }

        [Fact]
        public async void CalculateTotalPrice_ShouldReturnBidCalculatorDto()
        {
            // Arrange
            var obj = new BidCalculatorDto()
            {
                BasePrice = 398.00,
                VehiculeType = "common",
                AssociateFee = 5.00,
                BasicBuyerFee = 39.80,
                SellerSpecialFee = 7.96,
                StorageFee = 100
            };



            var vehicleAuctionGateway = A.Fake<IVehicleAuctionGateway>();
            A.CallTo(() => vehicleAuctionGateway.CalculateTotalPrice(398.00,"common")).Returns(obj);

            var vehicleAuctionService = new VehicleAuctionService(vehicleAuctionGateway);

            // Act
            var result = await vehicleAuctionService.CalculateTotalPrice(398.00, "common");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BidCalculatorDto>(result);

        }

        [Fact]
        public async void CalculateTotalPrice_ShouldReturnEmptyObject()
        {
            // Arrange
            var obj = new BidCalculatorDto();

            var vehicleAuctionGateway = A.Fake<IVehicleAuctionGateway>();
            A.CallTo(() => vehicleAuctionGateway.CalculateTotalPrice(0.00, "xxxx")).Returns(obj);

            var vehicleAuctionService = new VehicleAuctionService(vehicleAuctionGateway);

            // Act
            var result = await vehicleAuctionService.CalculateTotalPrice(0.00, "xxxx");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BidCalculatorDto>(result);

        }
    }
}
