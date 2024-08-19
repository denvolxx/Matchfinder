using Matchfinder.DTO;
using Matchfinder.Entities;

namespace Matchfinder.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser?> GetUserByIdAsync(int id);
        Task<AppUser?> GetUserByNameAsync(string username);

        Task<IEnumerable<MemberDTO>> GetMembersAsync();
        Task<MemberDTO?> GetMemberAsync(string username);
    }
}
