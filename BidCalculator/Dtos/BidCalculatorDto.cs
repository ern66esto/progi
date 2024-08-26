using BidCalculator.Gateway.Models;

namespace BidCalculator.Dtos
{
    public class BidCalculatorDto
    {
        public double BasePrice { get; set; }
        public string VehiculeType { get; set; }
        public double BasicBuyerFee { get; set; }
        public double SellerSpecialFee { get; set; }
        public double AssociateFee { get; set; }
        public double StorageFee { get; set; }
        public double Total { get 
            {
                return BasePrice + BasicBuyerFee + SellerSpecialFee + AssociateFee + StorageFee;
            } }
    }
}
