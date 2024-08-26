namespace BidCalculator.Gateway.Models
{
    public class BasicBuyerFee
    {
        public string Type { get; set; }
        public string Porcentage { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public string SellerSpecialFeePorcentage { get; set; }
    }
}
