using api.models;

namespace api.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}