using Microsoft.AspNetCore.Mvc;
using ItOffers.Features.StatisticsFeatures.Commands;
using MediatR;

namespace ItOffers.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DailyStatisticsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DailyStatisticsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<bool> PostStatistics()
        {
            var statisticsFeatures = await _mediator.Send(new PostNewStatisticsCommand());
            return statisticsFeatures;
        }
    }
}
