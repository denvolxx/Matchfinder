using Matchfinder.Entities;

namespace Matchfinder.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
