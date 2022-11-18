using ItOffers.Features.StatisticsFeatures.Commands;
using ItOffers.Models.Models;
using MediatR;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItOffers.Features.StatisticsFeatures.Queries
{
    public class GetStatisticsQuery : IRequest<IEnumerable<GetStatisticsModel>>
    {
        public class GetStatisticsQueryHandler : IRequestHandler<GetStatisticsQuery, IEnumerable<GetStatisticsModel>>
        {
            private IMongoClient _mongoClient;

            public GetStatisticsQueryHandler(IMongoClient mongoClient)
            {
                _mongoClient = mongoClient;
            }

            public async Task<IEnumerable<GetStatisticsModel>> Handle(GetStatisticsQuery request, CancellationToken cancellationToken)
            {
                var database = _mongoClient.GetDatabase("ItScrapper");
                var collection = database.GetCollection<GetStatisticsModel>("Statistics").AsQueryable().ToArray();
                //var avgSalary = collection.Select(o => o.AvgSalary).Average();
                //var avgSeniority = collection.GroupBy(o => o.Seniority).Select(g => new { Key = g.Key, Count = g.Count() }).OrderByDescending(x => x.Count).Take(3).ToList();
                //var statCollection = database.GetCollection<StatisticsModel>("Statistics");
                //await statCollection.InsertOneAsync(new StatisticsModel()
                //{
                //    Type = "Backend",
                //    Amount = collection.Length,
                //    AvgSalary = avgSalary,
                //    MostSeniorityOffers = avgSeniority[0].Key,
                //    MostSeniorityOffersCount = avgSeniority[0].Count,
                //    SecondSeniorityOffers = avgSeniority[1].Key,
                //    SecondSeniorityOffersCount = avgSeniority[1].Count,
                //    LeastSeniorityOffers = avgSeniority[2].Key,
                //    LeastSeniorityOffersCount = avgSeniority[2].Count,
                //    CreatedAt = DateTime.Now,
                //});
                return collection==null ? new List<GetStatisticsModel>() : collection;
            }
        }
    }
}
