using ItOffers.Models.Models;
using MediatR;
using MongoDB.Driver;

namespace ItOffers.Features.StatisticsFeatures.Commands
{
    public class PostNewStatisticsCommand : IRequest<bool>
    {
        public class PostNewStatisticsCommandHandler : IRequestHandler<PostNewStatisticsCommand, bool>
        {
            private IMongoClient _mongoClient;

            public PostNewStatisticsCommandHandler(IMongoClient mongoClient)
            {
                _mongoClient = mongoClient;
            }

            public async Task<bool> Handle(PostNewStatisticsCommand request, CancellationToken cancellationToken)
            {
                var scrapList = new List<StatisticsCollectionModel> {
                    new StatisticsCollectionModel { Type = "Backend", CollectionName = "BackendOffers" },
                    new StatisticsCollectionModel { Type = "Frontend", CollectionName = "FrontendOffers" },
                    new StatisticsCollectionModel { Type = "Fullstack", CollectionName = "FullStackOffers" },
                    new StatisticsCollectionModel { Type = "Mobile", CollectionName = "MobileOffers" }};

                var database = _mongoClient.GetDatabase("ItScrapper");
                foreach(var model in scrapList)
                {
                    var collection = database.GetCollection<GetOfferModel>(model.CollectionName).AsQueryable<GetOfferModel>().ToArray();
                    var avgSalary = collection.Select(o => o.AvgSalary).Average();
                    var avgSeniority = collection.GroupBy(o => o.Seniority).Select(g => new { Key = g.Key, Count = g.Count() }).OrderByDescending(x => x.Count).Take(3).ToList();
                    var statCollection = database.GetCollection<StatisticsModel>("Statistics");
                    await statCollection.InsertOneAsync(new StatisticsModel()
                    {
                        Type = model.Type,
                        Amount = collection.Length,
                        AvgSalary = avgSalary,
                        MostSeniorityOffers = avgSeniority[0].Key,
                        MostSeniorityOffersCount = avgSeniority[0].Count,
                        SecondSeniorityOffers = avgSeniority[1].Key,
                        SecondSeniorityOffersCount = avgSeniority[1].Count,
                        LeastSeniorityOffers = avgSeniority[2].Key,
                        LeastSeniorityOffersCount = avgSeniority[2].Count,
                        CreatedAt = DateTime.Now,
                    });
                }

                return true;
            }
        }
    }
}