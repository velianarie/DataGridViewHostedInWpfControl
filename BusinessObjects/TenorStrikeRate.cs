namespace DataGridViewHostedInWpfControl.BusinessObjects
{
    public class TenorStrikeRate
    {
        public TenorStrikeRate(string tenor, double strike, double rate)
        {
            Tenor = tenor;
            Strike = strike;
            Rate = rate;
        }

        public string Tenor { get; set; }

        public double Strike { get; set; }

        public double Rate { get; set; }
    }
}
