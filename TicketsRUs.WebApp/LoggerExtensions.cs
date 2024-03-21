public partial class ExampleHandler(ILogger<ExampleHandler> logger)
{
    public string HandleRequest()
    {
        LogHandleRequest(logger);
        LogHandleRequest2(logger, "number 2");
        LogHandleRequest3(logger, "number 3");
        logger.LogInformation("Luris was here!");
        return "Im here";
    }

    [LoggerMessage(LogLevel.Information, "ExampleHandler.HandleRequest was called")]
    public static partial void LogHandleRequest(ILogger logger);

    [LoggerMessage(LogLevel.Warning, "ExampleHandler.HandleRequest02 was called with {description}")]
    public static partial void LogHandleRequest2(ILogger logger, string description);

    [LoggerMessage(LogLevel.Error, "ExampleHandler.HandleRequest03 was called with {description}")]
    public static partial void LogHandleRequest3(ILogger logger, string description);
}

