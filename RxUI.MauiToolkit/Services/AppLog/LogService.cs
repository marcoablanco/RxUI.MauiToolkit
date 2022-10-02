namespace RxUI.MauiToolkit.Services.AppLog;

using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Crashes;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

internal class LogService<TCategoryName> : ILogService<TCategoryName>
{
	private readonly ILogger<TCategoryName> logger;
	private ObservableCollection<string> log;
	private int i = 0;

	public LogService(ILogger<TCategoryName> logger)
	{
		this.logger = logger;
		log = new ObservableCollection<string>();
	}


	public virtual void Log(string line)
	{
		string lineToWrite = $"{i++:D6}:{DateTime.UtcNow} - {line}";
		log.Add(lineToWrite);

		logger.LogDebug(lineToWrite);
	}

	public virtual void Event(string eventName)
	{
		string lineToWrite = $"{i++:D6}:{DateTime.UtcNow} - Event: {eventName}";
		log.Add(lineToWrite);

		logger.LogTrace(lineToWrite);
		Analytics.TrackEvent(eventName);
	}

	public void Warning(Exception ex)
	{
		string lineToWrite = $"{i++:D6}:{DateTime.UtcNow} - {GetExceptionData(ex)}";
		log.Add(lineToWrite);

		logger.LogWarning(ex, lineToWrite);

		Crashes.TrackError(ex, GetDeviceDataAndExceptionData(ex), ErrorAttachmentLog.AttachmentWithText(GetLog(), $"Log{DateTime.UtcNow.ToString("s")}.log"));
	}

	public void Error(Exception ex)
	{
		string lineToWrite = $"{i++:D6}:{DateTime.UtcNow} {GetExceptionData(ex)}";
		log.Add(lineToWrite);

		logger.LogError(ex, lineToWrite);

		Crashes.TrackError(ex, GetDeviceDataAndExceptionData(ex), ErrorAttachmentLog.AttachmentWithText(GetLog(), $"Log{DateTime.UtcNow.ToString("s")}.log"));
	}

	public void SetAppCenterId(string? ios = null, string? android = null, string? uwp = null, string? macos = null)
	{
		StringBuilder sb = new StringBuilder();
		if (!string.IsNullOrWhiteSpace(android))
			sb.Append($"android={android};");
		if (!string.IsNullOrWhiteSpace(ios))
			sb.Append($"ios={ios};");
		if (!string.IsNullOrWhiteSpace(uwp))
			sb.Append($"uwp={uwp};");
		if (!string.IsNullOrWhiteSpace(macos))
			sb.Append($"macos={macos};");

		AppCenter.Start(sb.ToString(), typeof(Analytics), typeof(Crashes));
	}

	protected virtual string GetLog()
	{
		StringBuilder sb = new StringBuilder();

		Dictionary<string, string> deviceProperties = GetDeviceProperties(new Dictionary<string, string>());
		foreach (KeyValuePair<string, string> item in deviceProperties)
		{
			sb.AppendLine($"{item.Key}: {item.Value}");
		}

		foreach (string item in log)
		{
			sb.AppendLine($"{item}");
		}
		return sb.ToString();
	}

	protected virtual Dictionary<string, string> GetDeviceDataAndExceptionData(Exception ex)
	{
		Dictionary<string, string> dict = new Dictionary<string, string>();
		dict = GetDeviceProperties(dict);
		dict.Add("EXCEPTION INFORMATION", GetExceptionData(ex));
		return dict;
	}

	protected virtual string GetExceptionData(Exception ex, string title = "EXCEPTION")
	{
		if (ex == null)
		{
			return string.Empty;
		}
		StringBuilder st = new StringBuilder();
		st.AppendLine($"--{title}--");
		st.AppendLine($"MESSAGE: {ex.Message}");
		st.AppendLine($"TOSTRING: {ex.ToString()}");
		st.AppendLine($"STACKTRACE: {ex.StackTrace}");
		if (ex.InnerException != null)
		{
			st.AppendLine(GetExceptionData(ex.InnerException, "INNER EXCEPTION"));
		}
		return st.ToString();
	}

	protected virtual Dictionary<string, string> GetDeviceProperties(Dictionary<string, string> properties)
	{
#if DEBUG
		properties.Add("Debug", "True");
#else
			properties.Add("Debug", "False");
#endif

		properties.Add("Device Idiom", DeviceInfo.Idiom.ToString());
		properties.Add("Device Platform", DeviceInfo.Platform.ToString());
		properties.Add("Device Type", DeviceInfo.DeviceType.ToString());
		properties.Add("Device Manufacturer", DeviceInfo.Manufacturer);
		properties.Add("Device Model", DeviceInfo.Model);
		properties.Add("Device Name", DeviceInfo.Name);
		properties.Add("Device Version", DeviceInfo.Version.ToString());
		properties.Add("Device VersionString", DeviceInfo.VersionString);

		return properties;
	}
}