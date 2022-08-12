using Microsoft.AspNetCore.Mvc;
using MediatR;
using ItOffers.Features.ScrapperOffersFeatures.Commands;

namespace ItOffers.Controllers
{
    
    
    [ApiController]
    [Route("[controller]")]
    public class NoFluffJobsController : ControllerBase
    {

        private readonly IMediator _mediator;

        public NoFluffJobsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<bool> PostNoFluffJobsOffers()
        {
            var result = await _mediator.Send(new ScrappNoFluffJobsCommand());
            return result;
        }

    }
    
}
