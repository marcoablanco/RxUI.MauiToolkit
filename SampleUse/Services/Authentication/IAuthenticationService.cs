namespace SampleUse.Services.Authentication;

using System.Threading.Tasks;

internal interface IAuthenticationService
{
    Task<Token> GetTokenAsync(string user, string password);
    Task<Token> RefreshTokenAsync(string refreshToken);
}