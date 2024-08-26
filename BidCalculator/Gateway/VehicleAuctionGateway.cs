using BidCalculator.Gateway.Models;
using System.Globalization;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using BidCalculator.Dtos;
namespace BidCalculator.Gateway
{
    public class VehicleAuctionGateway : IVehicleAuctionGateway
    {
        private readonly IConfiguration _configuration;
        
        public VehicleAuctionGateway(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<BidCalculatorDto> CalculateTotalPrice(double basePrice, string type)
        {
            var bidCalculateModel = new BidCalculateModel();
            BidCalculatorDto bidCalculatorDto;
            try
            {
                _configuration.GetSection(BidCalculateModel.BidCalculatorJson).Bind(bidCalculateModel);

                var basicBuyerFee = await CalculateBasicBuyerFee(basePrice, type, bidCalculateModel);
                var specialFee = await CalculateSpecialFee(basePrice, type, bidCalculateModel);
                var associateFee = await CalculateAssociatedFee(basePrice, bidCalculateModel);
                var storageFee = bidCalculateModel.Storage;

                bidCalculatorDto = new BidCalculatorDto()
                {
                    BasePrice = basePrice,
                    VehiculeType = type,
                    BasicBuyerFee = basicBuyerFee,
                    SellerSpecialFee = specialFee,
                    AssociateFee = associateFee,
                    StorageFee = bidCalculateModel.Storage
                };
            }
            catch (Exception)
            {

                bidCalculatorDto = new BidCalculatorDto();
            }
            

            return bidCalculatorDto;
        }

        private async Task<double> CalculateBasicBuyerFee(double basePrice, string type, BidCalculateModel bidCalculateModel)
        {
            double perct = 0.00;
            int min = 0;
            int max = 0;
            double basicBuyerFee;

            switch (type.ToLower())
            {
                case "common":
                    perct = await Task.Run(() => double.Parse(bidCalculateModel.Fees.BasicBuyerFee[0].Porcentage, CultureInfo.InvariantCulture.NumberFormat));
                    min = bidCalculateModel.Fees.BasicBuyerFee[0].Min;
                    max = bidCalculateModel.Fees.BasicBuyerFee[0].Max;
                    break;
                case "luxury":
                    perct = await Task.Run(() => double.Parse(bidCalculateModel.Fees.BasicBuyerFee[1].Porcentage, CultureInfo.InvariantCulture.NumberFormat));
                    min = bidCalculateModel.Fees.BasicBuyerFee[1].Min;
                    max = bidCalculateModel.Fees.BasicBuyerFee[1].Max;
                    break;
                default:
                    break;
            }

            basicBuyerFee = basePrice * perct;
            if (basicBuyerFee < min) { basicBuyerFee = min; }
            else if(basicBuyerFee > max) { basicBuyerFee = max;}

            return Math.Round(basicBuyerFee,2);
        }

        private async Task<double> CalculateSpecialFee(double basePrice, string type, BidCalculateModel bidCalculateModel)
        {
            double specialFee = 0.00;
            double special = 00;
            switch (type.ToLower())
            {
                case "common":
                    special = await Task.Run(() => double.Parse(bidCalculateModel.Fees.BasicBuyerFee[0].SellerSpecialFeePorcentage, CultureInfo.InvariantCulture.NumberFormat));

                    break;
                case "luxury":
                    special = await Task.Run(() => double.Parse(bidCalculateModel.Fees.BasicBuyerFee[1].SellerSpecialFeePorcentage, CultureInfo.InvariantCulture.NumberFormat));

                    break;
                default:
                    break;
            }
            specialFee = basePrice * special;
            return specialFee;
        }
        private async Task<double> CalculateAssociatedFee(double basePrice, BidCalculateModel bidCalculateModel)
        {
            double associateFee = 00.0;
            for (int i = 0; i < bidCalculateModel.Fees.AssociateFee.Count(); i++)
            {
                List<int> intList = await Task.Run(() => JsonConvert.DeserializeObject<List<int>>(bidCalculateModel.Fees.AssociateFee[i].AmountRange));
                var price = bidCalculateModel.Fees.AssociateFee[i].Price;
                if(intList.Count() == 2 &&  basePrice <= intList.Max() && basePrice >= intList.Min())
                {
                    associateFee = bidCalculateModel.Fees.AssociateFee[i].Price; break;
                }
                else if(intList.Count() == 1 && basePrice >= intList.First())
                {
                    associateFee = bidCalculateModel.Fees.AssociateFee[i].Price; break;
                }
            }
            return associateFee;
        }
    }
}
