namespace SampleUse.Features.Login;

using System.Threading.Tasks;

internal interface ILoginService
{
	Task LoginAsync(string user, string password);
	Task AutoLoginAsync();
}