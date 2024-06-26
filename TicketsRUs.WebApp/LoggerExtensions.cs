public partial class ExampleHandler(ILogger<ExampleHandler> logger)
{
    public string HandleRequest()
    {
        LogHandleRequest(logger);
        LogHandleRequest2(logger, "number 2");
        LogHandleRequest3(logger, "number 3");
        LogHandleRequest4(logger, "number 4");
        LogHandleRequest5(logger, "number 5");
        logger.LogInformation("Luris was here!");
        return "Im here";
    }

    [LoggerMessage(LogLevel.Information, "ExampleHandler.HandleRequest was called")]
    public static partial void LogHandleRequest(ILogger logger);

    [LoggerMessage(LogLevel.Warning, "ExampleHandler.HandleRequest02 was called with {description}")]
    public static partial void LogHandleRequest2(ILogger logger, string description);

    [LoggerMessage(LogLevel.Critical, "ExampleHandler.HandleRequest03 was called with {description}")]
    public static partial void LogHandleRequest3(ILogger logger, string description);

    [LoggerMessage(LogLevel.Trace, "ExampleHandler.HandleRequest04 was called with {description}")]
    public static partial void LogHandleRequest4(ILogger logger, string description);

    [LoggerMessage(LogLevel.Error, "ExampleHandler.HandleRequest05 was called with {description}")]
    public static partial void LogHandleRequest5(ILogger logger, string description);
}

