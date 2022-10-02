namespace RxUI.MauiToolkit.Services.AppLog;

using System;

public interface ILogService
{
	void Log(string line);
	void Event(string eventName);
	void Warning(Exception ex);
	void Error(Exception ex);
	void SetAppCenterId(string? ios = null, string? android = null, string? uwp = null, string? macos = null);
}
public interface ILogService<TCategoryName> : ILogService
{
}