namespace BidCalculator.Gateway.Models
{
    public class BidCalculateModel
    {
        public const string BidCalculatorJson = "BidCalculatorJson";
        public double BasePrice { get; set; }
        public int Storage { get; set; }
        public Fees Fees { get; set; }
    }
}
