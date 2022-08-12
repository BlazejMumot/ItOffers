using ItOffers.Features.NoFluffJobsFeatures.Commands;
using ItOffers.Models.Models;
using MediatR;
using MongoDB.Driver;


namespace ItOffers.Features.ScrapperOffersFeatures.Commands
{
    public class ScrappNoFluffJobsCommand : IRequest<bool>
    {
        public class ScrappNoFluffJobsCommandHandler : IRequestHandler<ScrappNoFluffJobsCommand, bool>
        {
            private IMongoClient _mongoClient;

            public ScrappNoFluffJobsCommandHandler(IMongoClient mongoClient)
            {
                _mongoClient = mongoClient;
            }

            public async Task<bool> Handle(ScrappNoFluffJobsCommand request, CancellationToken cancellationToken)
            {
                var database = _mongoClient.GetDatabase("ItScrapper");
                var collection = database.GetCollection<ScrappOfferModel>("BackendOffers");

                collection.DeleteMany("{}");

                var scrappOffersToDBCommand = new ScrappOffersToDBCommand();
                var offersNoFluff = scrappOffersToDBCommand.GetBackendOffers();

                collection.InsertMany(offersNoFluff);

                var logsCollection = database.GetCollection<LogsModel>("Logs");
                logsCollection.InsertOne(new LogsModel() { Action = "Add Offers", Type = "Backend", Site = "NoFluffJobs", Amount = offersNoFluff.ToArray().Length, DateTime = DateTime.Now });

                return true;
            }
        }
    }
}
