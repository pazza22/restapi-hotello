using HotelAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelAPI
{
    public class HotelApiDBContext : DbContext
    {
        public HotelApiDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<RoomEntity> Rooms { get; set; }
    }
}
