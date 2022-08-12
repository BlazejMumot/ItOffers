using Microsoft.AspNetCore.Mvc;
using ItOffers.Features.ScrapperOffersFeatures;


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

        [HttpGet]
        public bool Get()
        {
            var scrappOffersFeature = new ScrappOffersFeature();
            scrappOffersFeature.GetOffersfromWebistesAsync();
            return Guid.NewGuid();
        }
    }
}
