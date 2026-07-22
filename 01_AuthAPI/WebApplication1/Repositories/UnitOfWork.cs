using AuthAPI.Repositories.Interfaces;
using AuthAPI.Models;
using AuthAPI.DATA;
namespace AuthAPI.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IGenericRepository<User> Users { get; private set; }
        public IGenericRepository<ClubEvent> ClubEvents { get; private set; }
        public IGenericRepository<Reward> Rewards { get; private set; }
        public IGenericRepository<RoomListing> RoomListing { get; private set; }
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Users = new GenericRepository<User>(_context);
            ClubEvents = new GenericRepository<ClubEvent>(_context);
            Rewards = new GenericRepository<Reward>(_context);
            RoomListing = new GenericRepository<RoomListing>(_context);
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