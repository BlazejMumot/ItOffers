using ItOffers.Models.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItOffers.Features.StatisticsFeatures.Commands
{
    public class PostNewStatisticsCommand
    {
        public bool CreateStatistics()
        {
            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://tofik:piesek@cluster0.fcnjisn.mongodb.net/?retryWrites=true&w=majority");
            var client = new MongoClient(settings);
            var database = client.GetDatabase("ItScrapper");
            var collection = database.GetCollection<GetOfferModel>("BackendOffers").AsQueryable<GetOfferModel>().ToArray();
            var avgSalary = collection.Select(o => o.AvgSalary).Average();
            var avgSeniority = collection.GroupBy(o => o.Seniority).Select(g => new { Key = g.Key, Count = g.Count() }).OrderByDescending(x => x.Count).Take(3).ToList();
            var statCollection = database.GetCollection<StatisticsModel>("Statistics");
            statCollection.InsertOne(new StatisticsModel()
            {
                Type = "Backend",
                Amount = collection.Length,
                AvgSalary = avgSalary,
                MostSeniorityOffers = avgSeniority[0].Key,
                MostSeniorityOffersCount = avgSeniority[0].Count,
                SecondSeniorityOffers = avgSeniority[1].Key,
                SecondSeniorityOffersCount = avgSeniority[1].Count,
                LeastSeniorityOffers = avgSeniority[2].Key,
                LeastSeniorityOffersCount = avgSeniority[2].Count,
                CreatedAt = DateOnly.FromDateTime(DateTime.Now)
        });
            return true;
        }
    }
}