using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelAPI.Controllers
{
    [ApiController]
    [Route("/")]
    public class RouteController : ControllerBase
    {
        [HttpGet(Name = nameof(GetRoot))]
        [ProducesResponseType(200)]
        public IActionResult GetRoot()
        {
            var response = new
            {
                href = Url.Link(nameof(GetRoot), null),
                rooms = new
                {
                    href = Url.Link(nameof(RoomsController.GetRooms), null)
                },
                info = new
                {
                    href = Url.Link(nameof(InfoController.GetInfo), null)
                }
            };
            return Ok(response);
        }
    }
}
