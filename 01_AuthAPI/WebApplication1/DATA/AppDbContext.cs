using AuthAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using AuthAPI.Security;
namespace AuthAPI.DATA 
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Reward> Rewards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var fixedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var defaultPassword = BCrypt.Net.BCrypt.HashPassword("123456");

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "admin", Email = "admin@gmail.com", Password = defaultPassword, Role = Roles.Admin, ExpiryTime = fixedDate },
                new User { Id = 2, Username = "captain_a", Email = "captain_a@gmail.com", Password = defaultPassword, Role = Roles.Captain, ExpiryTime = fixedDate },
                new User { Id = 3, Username = "captain_b", Email = "captain_b@gmail.com", Password = defaultPassword, Role = Roles.Captain, ExpiryTime = fixedDate },
                new User { Id = 4, Username = "player_a", Email = "player_a@gmail.com", Password = defaultPassword, Role = Roles.User, ExpiryTime = fixedDate },
                new User { Id = 5, Username = "player_b", Email = "player_b@gmail.com", Password = defaultPassword, Role = Roles.User, ExpiryTime = fixedDate }
            );

            modelBuilder.Entity<Team>().HasData(
                new Team { Id = 1, TeamName = "Team 1", CaptainId = 2, LogoURL = string.Empty },
                new Team { Id = 2, TeamName = "Team 2", CaptainId = 3, LogoURL = string.Empty }
            );

            modelBuilder.Entity<TeamMember>().HasData(
                new TeamMember { Id = 1, TeamId = 1, UserId = 2, InGameName = "captain_a" }, // captain_a 
                new TeamMember { Id = 2, TeamId = 1, UserId = 4, InGameName = "player_a" },  // player_a 
                new TeamMember { Id = 3, TeamId = 2, UserId = 3, InGameName = "captain_b" }, // captain_b 
                new TeamMember { Id = 4, TeamId = 2, UserId = 5, InGameName = "player_b" }    // player_b 
            );
        }
    }
}