using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReservationService.Reservations.Commands;
using ReservationService.Reservations.Models;
using ReservationService.Reservations.Queries;

namespace ReservationService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReservationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationDTO>> GetReservationById(int id)
        {
            var query = new GetReservationByIdQuery(id);
            var reservation = await _mediator.Send(query);
            if (reservation == null) return NotFound();
            return Ok(reservation);
        }

        [HttpPost]
        public async Task<ActionResult<ReservationDTO>> CreateReservation(ReservationDTO reservationDTO)
        {
            var command = new CreateReservationCommand(reservationDTO);
            var createdReservation = await  _mediator.Send(command);
            return CreatedAtAction(nameof(GetReservationById), new { id = createdReservation.Id }, createdReservation);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReservation(int id)
        {
            var command = new RemoveReservationCommand(id);
            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservations([FromQuery] DateTime? date, [FromQuery] string status)
        {
            var query = new GetReservationsQuery(date, status);
            var reservations = await _mediator.Send(query);
            return Ok(reservations);
        }
    }
}