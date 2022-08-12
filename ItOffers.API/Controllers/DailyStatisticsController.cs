using Microsoft.AspNetCore.Mvc;
using ItOffers.Features.StatisticsFeatures.Commands;


namespace ItOffers.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DailyStatisticsController : ControllerBase
    {
        private readonly ILogger<DailyStatisticsController> _logger;

        public DailyStatisticsController(ILogger<DailyStatisticsController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public bool Get()
        {
            var statisticsFeatures = new PostNewStatisticsCommand();
            statisticsFeatures.CreateStatistics();
            return true;
        }
    }
}
