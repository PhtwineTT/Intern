using AuthAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace AuthAPI.DATA
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<ClubEvent> ClubEvents { get; set; }
        public DbSet<RoomListing> RoomListings { get; set; }
        public DbSet<Reward> Rewards { get; set; }
    }
}
