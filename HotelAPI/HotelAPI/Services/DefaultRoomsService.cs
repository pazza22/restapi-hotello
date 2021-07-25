using AutoMapper;
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
        private readonly IMapper _mapper;

        public DefaultRoomsService(HotelApiDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Room> GetRoomAsync(Guid id)
        {
            var entity = await _context.Rooms.SingleOrDefaultAsync(r => r.Id == id);

            if (entity == null)
            {
                return null;
            }

            return _mapper.Map<Room>(entity);
        }
    }
}
