using AutoMapper;
using AutoMapper.QueryableExtensions;
using Matchfinder.DTO;
using Matchfinder.Entities;
using Matchfinder.Helpers;
using Matchfinder.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Matchfinder.Data
{
    public class UserRepository(DataContext context, IMapper mapper) : IUserRepository
    {
        public async Task<MemberDTO?> GetMemberAsync(string username)
        {
            return await context.Users
                .Where(x => x.UserName == username)
                .ProjectTo<MemberDTO>(mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<PagedList<MemberDTO>> GetMembersAsync(UserParams userParams)
        {
            IQueryable<AppUser> query = context.Users.Where(x => x.UserName != userParams.CurrentUsername);
            if (userParams.Gender != null)
            {
                query = query.Where(x => x.Gender == userParams.Gender);
            }

            var minDob = DateOnly.FromDateTime(DateTime.Today.AddYears(-userParams.MaxAge - 1));
            var maxDob = DateOnly.FromDateTime(DateTime.Today.AddYears(-userParams.MinAge));

            query = query.Where(x => x.DateOfBirth >= minDob && x.DateOfBirth <= maxDob);

            return await PagedList<MemberDTO>.CreateAsync(query.ProjectTo<MemberDTO>(mapper.ConfigurationProvider), userParams.PageNumber, userParams.PageSize);
        }

        public async Task<AppUser?> GetUserByIdAsync(int id)
        {
            return await context.Users.FindAsync(id);
        }

        public async Task<AppUser?> GetUserByNameAsync(string username)
        {
            return await context.Users.Include(x => x.Photos).SingleOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await context.Users.Include(x => x.Photos).ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            bool isSaved = await context.SaveChangesAsync() > 0;
            return isSaved;
        }

        public void Update(AppUser user)
        {
            context.Entry(user).State = EntityState.Modified;
        }
    }
}
