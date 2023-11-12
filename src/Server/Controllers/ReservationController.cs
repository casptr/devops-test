using Azure.Core;
using Foodtruck.Shared.Formulas;
using Foodtruck.Shared.Reservations;
using Foodtruck.Shared.Supplements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Foodtruck.Server.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController
    {
        private readonly IReservationService reservationService;

        public ReservationController(IReservationService reservationService)
        {
            this.reservationService = reservationService;
        }

        [SwaggerOperation("Returns all reservations")]
        [HttpGet, AllowAnonymous]
        public async Task<ReservationResult.Index> GetIndex([FromQuery] ReservationRequest.Index request)
        {
            return await reservationService.GetIndexAsync(request);
        }


        [SwaggerOperation("Returns all detailed reservations for admin")]
        [HttpGet("detailed"), AllowAnonymous] // TODO Only admins should be able to get this
        public async Task<ReservationResult.Detailed> GetAdminIndex([FromQuery] ReservationRequest.Detailed request)
        {
            return await reservationService.GetDetailedAsync(request);
        }
    }
}
