using Microsoft.AspNetCore.Mvc;
using ReservationService.Reservations.Commands;
using ReservationService.Reservations.Models;
using ReservationService.Reservations.Queries;

namespace ReservationService.Controllers
{
    public class ReservationController : ApiController
    {
        [HttpGet]
        [Route("getAllReservations")]
        public async Task<ActionResult<List<ReservationDTO>>> GetAllReservations([FromQuery] GetAllReservationsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet]
        [Route("getReservationById")]
        public async Task<ActionResult<ReservationDTO>> GetReservationById([FromQuery] GetReservationByIdQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        [Route("createReservation")]
        public async Task<ActionResult<ReservationDTO>> CreateReservation([FromBody] CreateReservationCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReservation(int id)
        {
            var command = new RemoveReservationCommand(id);
            var result = await Mediator.Send(command);
            if (!result) return NotFound();
            return Ok();
        }

        [HttpGet]
        [Route("getReservationsByDate")]
        public async Task<ActionResult<List<ReservationDTO>>> GetReservationsByDate([FromQuery] GetReservationsByDateQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}