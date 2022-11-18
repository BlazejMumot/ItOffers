using Microsoft.AspNetCore.Mvc;
using ItOffers.Features.StatisticsFeatures.Commands;
using MediatR;
using MongoDB.Driver;
using ItOffers.Features.StatisticsFeatures.Queries;
using ItOffers.Models.Models;

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

        [HttpGet]
        public async Task<IEnumerable<GetStatisticsModel>> GetStatistics()
        {
            var response = await _mediator.Send(new GetStatisticsQuery());
            return response;
        }
    }
}
