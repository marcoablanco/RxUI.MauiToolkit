namespace SampleUse.Services.Persistance;

using SampleUse.Services.Authentication;
using System.Threading.Tasks;

internal interface IPersistanceService
{
	Task<Token?> GetTokenAsync();
	Task SaveTokenAsync(Token token);
}