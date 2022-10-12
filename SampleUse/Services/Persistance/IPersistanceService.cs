namespace SampleUse.Services.Persistance;

using SampleUse.Services.Authentication;
using System.Threading.Tasks;

internal interface IPersistanceService
{
	// Token
	Task<Token?> GetTokenAsync();
	Task SaveTokenAsync(Token token);


	// User
	Task<string?> GetUserAsync();
	Task SaveUserAsync(string user);
}