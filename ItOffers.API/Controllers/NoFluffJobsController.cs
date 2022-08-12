using Microsoft.AspNetCore.Mvc;
using ItOffers.Features.ScrapperOffersFeatures;

namespace ItOffers.Controllers
{
    
    
    [ApiController]
    [Route("[controller]")]
    public class NoFluffJobsController : ControllerBase
    {
           
        private readonly ILogger<NoFluffJobsController> _logger;

        public NoFluffJobsController(ILogger<NoFluffJobsController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public bool GetGuid()
        {
            var scrappOffersFeature = new ScrappOffersFeature();
            var result = scrappOffersFeature.GetOffersfromWebistesAsync();
            return result;
        }

    }
    
}
