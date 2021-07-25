using HotelAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelAPI.Services
{
    public class DefaultRoomsService : IRoomService
    {
        private readonly HotelApiDBContext _context;

        public DefaultRoomsService(HotelApiDBContext context)
        {
            _context = context;
        }

        public async Task<Room> GetRoomAsync(Guid id)
        {
            var entity = await _context.Rooms.SingleOrDefaultAsync(r => r.Id == id);

            if (entity == null)
            {
                return null;
            }

            return new Room
            {
                Name = entity.Name,
                Rate = entity.Rate / 100.0m,
                Href = null
            };
        }
    }
}
