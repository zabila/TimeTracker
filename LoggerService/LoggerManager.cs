using Contracts;
using NLog;

namespace LoggerService;

public class LoggerManager : ILoggerManager
{
    private readonly ILogger _loggerManagerImplementation = LogManager.GetCurrentClassLogger();

    public void LogDebug(string message)
    {
        _loggerManagerImplementation.Debug(message);
    }

    public void LogError(string message)
    {
        _loggerManagerImplementation.Error(message);
    }

    public void LogInfo(string message)
    {
        _loggerManagerImplementation.Info(message);
    }

    public void LogWarn(string message)
    {
        _loggerManagerImplementation.Warn(message);
    }
}