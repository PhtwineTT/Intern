using AuthAPI.DATA;
using AuthAPI.Models;
using AuthAPI.Repositories.Interfaces;
namespace AuthAPI.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IGenericRepository<User> Users { get; private set; }
        public IGenericRepository<Tournament> Tournaments { get; private set; }
        public IGenericRepository<Team> Teams { get; private set; }
        public IGenericRepository<TeamMember> TeamMembers { get; private set; }
        public IGenericRepository<Venue> Venues { get; private set; }
        public IGenericRepository<Reward> Rewards { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Users = new GenericRepository<User>(_context);
            Tournaments = new GenericRepository<Tournament>(_context);
            Teams = new GenericRepository<Team>(_context);
            TeamMembers = new GenericRepository<TeamMember>(_context);
            Venues = new GenericRepository<Venue>(_context);
            Rewards = new GenericRepository<Reward>(_context);
        }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}