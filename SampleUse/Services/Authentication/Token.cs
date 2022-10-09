namespace SampleUse.Services.Authentication;

using System;

public class Token
{
    public string OAuth { get; internal set; }
    public string RefreshToken { get; internal set; }
    public DateTimeOffset ValidAt { get; internal set; }
    public DateTimeOffset RefreshValidAt { get; internal set; }
}