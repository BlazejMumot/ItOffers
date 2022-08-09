using ItOffers.Features.NoFluffJobsFeatures.Commands;
using ItOffers.Models.Models;
using MongoDB.Driver;

namespace ItOffers.Features.ScrapperOffersFeatures
{
    public class ScrappOffersFeature
    {
        public Guid GetOffersfromWebistesAsync()
        {
            var scrappOffersToDBCommand = new ScrappOffersToDBCommand();

            var offersNoFluff = scrappOffersToDBCommand.GetBackendOffers();

            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://tofik:piesek@cluster0.fcnjisn.mongodb.net/?retryWrites=true&w=majority");
            var client = new MongoClient(settings);
            var database = client.GetDatabase("ItScrapper");
            var collection = database.GetCollection<ScrappOfferModel>("BackendOffers");

            collection.DeleteMany("{}");
            collection.InsertMany(offersNoFluff);

            return Guid.Empty;
        }

    }
}
