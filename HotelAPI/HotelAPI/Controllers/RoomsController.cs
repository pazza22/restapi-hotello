using HotelAPI.Models;
using HotelAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelAPI.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet(Name = nameof(GetRooms))]
        public IActionResult GetRooms()
        {
            var response = new
            {
                href = Url.Link(nameof(GetRooms), null)
            };
            return Ok(response);
        }

        //Get /rooms/{roomID}
        [HttpGet("{roomId}", Name = nameof(GetRoomByID))]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Room>> GetRoomByID(Guid roomId)
        {
            var room = await _roomService.GetRoomAsync(roomId);

            if(room == null)
            {
                return NotFound();
            }

            room.Href = Url.Link(nameof(GetRoomByID), new { roomId = roomId });
            return room;
        }
    }
}
