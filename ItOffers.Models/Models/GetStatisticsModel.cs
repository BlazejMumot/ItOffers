using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItOffers.Models.Models
{
    public class GetStatisticsModel
    {
        public ObjectId _id { get; set; }
        public string Type { get; set; }
        public int Amount { get; set; }
        public double? AvgSalary { get; set; }
        public string MostSeniorityOffers { get; set; }
        public int MostSeniorityOffersCount { get; set; }
        public string SecondSeniorityOffers { get; set; }
        public int SecondSeniorityOffersCount { get; set; }
        public string LeastSeniorityOffers { get; set; }
        public int LeastSeniorityOffersCount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
