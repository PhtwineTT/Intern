using Microsoft.EntityFrameworkCore;
using Module.Models;
namespace Module
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<RoomListing> RoomListing { get; set; }
        public DbSet<ClubEvent> ClubEvents { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Reward> Rewards { get; set; }
    }
}
