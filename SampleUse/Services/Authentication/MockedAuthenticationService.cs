namespace SampleUse.Services.Authentication;
using System;

internal class MockedAuthenticationService : IAuthenticationService
{
    public async Task<Token> GetTokenAsync(string user, string password)
    {
        if (string.IsNullOrWhiteSpace(user))
            throw new ArgumentException(nameof(user));

        if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
            throw new ArgumentException(nameof(password));

		await Task.Delay(TimeSpan.FromSeconds(2));

		if (user != "Marco" || password != "1234")
            throw new InvalidOperationException();
		
        await Task.Delay(TimeSpan.FromSeconds(1));

		return new Token
        {
            OAuth = "Soy el mejor Token que verás jamás",
            RefreshToken = "Soy un token de refresco, salgo en el segundo tiempo.",
            ValidAt = new DateTimeOffset().AddHours(12),
            RefreshValidAt = new DateTimeOffset().AddMonths(3)
        };
    }

    public async Task<Token> RefreshTokenAsync(string refreshToken)
    {
        if (string.IsNullOrWhiteSpace(refreshToken))
            throw new ArgumentException(nameof(refreshToken));

		await Task.Delay(TimeSpan.FromSeconds(2));

		return await Task.FromResult(new Token
        {
            OAuth = "Soy el mejor Token que verás jamás",
            RefreshToken = "Soy un token de refresco, salgo en el segundo tiempo.",
            ValidAt = new DateTimeOffset().AddHours(12),
            RefreshValidAt = new DateTimeOffset().AddMonths(3)
        });
    }
}
