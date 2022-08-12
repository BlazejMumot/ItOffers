namespace ItOffers.Models.Models
{
    public class StatisticsModel
    {
        public string Type { get; set; }
        public int Amount { get; set; }
        public double? AvgSalary { get; set; }
        public string MostSeniorityOffers { get; set; }
        public int MostSeniorityOffersCount { get; set; }
        public string SecondSeniorityOffers { get; set; }
        public int SecondSeniorityOffersCount { get; set; }
        public string LeastSeniorityOffers { get; set; }
        public int LeastSeniorityOffersCount { get; set; }
        public DateOnly CreatedAt { get; set; }
    }
}
