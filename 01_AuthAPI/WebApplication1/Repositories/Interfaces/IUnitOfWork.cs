using AuthAPI.Models;

namespace AuthAPI.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> Users { get; }
        IGenericRepository<Tournament> Tournaments { get; }
        IGenericRepository<Team> Teams { get; }
        IGenericRepository<TeamMember> TeamMembers { get; }
        IGenericRepository<Venue> Venues { get; }
        IGenericRepository<Reward> Rewards { get; }
        Task<int> CompleteAsync();
    }
}
