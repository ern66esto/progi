using BidCalculator.Dtos;
using BidCalculator.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BidCalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleAuctionController : ControllerBase
    {
        private readonly IVehicleAuctionService _vehicleAuctionService;

        public VehicleAuctionController(IVehicleAuctionService vehicleAuctionService)
        {
            _vehicleAuctionService = vehicleAuctionService;
        }

        // GET api/<VehicleAuctionController>/5
        [HttpGet("{basePrice}/{type}")]
        public async Task< ActionResult<BidCalculatorDto>> GetVehicleAuction(double basePrice, string type)
        {
            BidCalculatorDto bidCalculatorDto = await _vehicleAuctionService.CalculateTotalPrice(basePrice, type);

            if(bidCalculatorDto == null)
            {
                return NotFound();
            }

            return bidCalculatorDto;
        }
        
    }
}
