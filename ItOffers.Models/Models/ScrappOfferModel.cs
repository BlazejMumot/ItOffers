namespace ItOffers.Models.Models
{
    public class ScrappOfferModel
    {
            public ScrappOfferModel(string offerName, string company, string seniority, double? minSalary,
                double? maxSalary, double? avgSalary, string currency, string tech, string location, string url)
            {
                OfferName = offerName;
                Company = company;
                Seniority = seniority;
                MinSalary = minSalary;
                MaxSalary = maxSalary;
                AvgSalary = avgSalary;
                Currency = currency;
                Tech = tech;
                Location = location;
                Url = url;
            }
            public string OfferName { get; set; }
            public string Company { get; set; }
            public string Seniority { get; set; }
            public double? MinSalary { get; set; }
            public double? MaxSalary { get; set; }
            public double? AvgSalary { get; set; }
            public string Currency { get; set; }
            public string Tech { get; set; }
            public string Location { get; set; }
            public string Url { get; set; }
    }
}
