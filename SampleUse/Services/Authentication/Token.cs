namespace SampleUse.Services.Authentication;

using System;

public class Token
{
    public string OAuth { get; set; }
    public string RefreshToken { get; set; }
    public DateTimeOffset ValidAt { get; set; }
    public DateTimeOffset RefreshValidAt { get; set; }
	public string Username { get; set; }
}