
namespace ItOffers.Models.Models
{
    public class SiteToScrappModel
    {
        public SiteToScrappModel(string url, string database, string type)
        {
            Url = url;
            Database = database;
            Type = type;
        }

        public string Url { get; set; }
        public string Database { get; set; }
        public string Type { get; set; }
    }
}
