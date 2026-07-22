using AuthAPI.Models;

namespace AuthAPI.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> Users { get; }
        IGenericRepository<ClubEvent> ClubEvents { get; }
        IGenericRepository<Reward> Rewards { get; }
        IGenericRepository<RoomListing> RoomListing { get; }
        Task<int> CompleteAsync();
    }
}
