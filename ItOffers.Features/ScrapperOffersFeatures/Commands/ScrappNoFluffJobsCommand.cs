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
            public IEnumerable<SiteToScrappModel> modelToScrap = new SiteToScrappModel[]
                {
                    new SiteToScrappModel("backend","BackendOffers","Backend"),
                    new SiteToScrappModel("frontend","FrontendOffers","Frontend"),
                    new SiteToScrappModel("fullstack","FullStackOffers","Fullstack"),
                    new SiteToScrappModel("mobile","MobileOffers","Mobile"),
                };

            public ScrappNoFluffJobsCommandHandler(IMongoClient mongoClient)
            {
                _mongoClient = mongoClient;
            }

            public async Task<bool> Handle(ScrappNoFluffJobsCommand request, CancellationToken cancellationToken)
            {
                var database = _mongoClient.GetDatabase("ItScrapper");
                foreach (var model in modelToScrap)
                {
                    var scrappOffersToDBCommand = new ScrappOffersToDBCommand();
                    var offersNoFluff = await scrappOffersToDBCommand.GetBackendOffersAsync(model.Url);

                    var collection = database.GetCollection<ScrappOfferModel>(model.Database);
                    await collection.DeleteManyAsync("{}");

                   
                    await collection.InsertManyAsync(offersNoFluff);


                    var logsCollection = database.GetCollection<LogsModel>("Logs");
                    await logsCollection.InsertOneAsync(new LogsModel() { Action = "Add Offers",
                        Type = model.Type, Site = "NoFluffJobs", Amount = offersNoFluff.ToArray().Length, DateTime = DateTime.Now });
                }

                return true;
            }
        }
    }
}
