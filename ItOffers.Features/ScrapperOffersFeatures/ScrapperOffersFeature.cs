using ItOffers.Features.NoFluffJobsFeatures.Commands;
using ItOffers.Models.Models;
using MongoDB.Driver;

namespace ItOffers.Features.ScrapperOffersFeatures
{
    public class ScrappOffersFeature
    {
        public bool GetOffersfromWebistesAsync()
        {
            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://tofik:piesek@cluster0.fcnjisn.mongodb.net/?retryWrites=true&w=majority");
            var client = new MongoClient(settings);
            var database = client.GetDatabase("ItScrapper");
            var collection = database.GetCollection<ScrappOfferModel>("BackendOffers");

            collection.DeleteMany("{}");

            var scrappOffersToDBCommand = new ScrappOffersToDBCommand();

            var offersNoFluff = scrappOffersToDBCommand.GetBackendOffers();

            collection.InsertMany(offersNoFluff);

            var logsCollection = database.GetCollection<LogsModel>("Logs");
            logsCollection.InsertOne(new LogsModel(){ Action="Add Offers",Type="Backend",Site="NoFluffJobs",Amount=offersNoFluff.ToArray().Length,DateTime =DateTime.Now});

            return true;
        }

    }
}
