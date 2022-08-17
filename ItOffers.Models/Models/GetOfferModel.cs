using MongoDB.Bson;

namespace ItOffers.Models.Models
{
    public class GetOfferModel
    {
        public ObjectId _id { get; set; }
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
        public string WebSite { get; set; }
    }
}
