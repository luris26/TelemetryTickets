public partial class ExampleHandler(ILogger<ExampleHandler> logger)
{
    public string HandleRequest()
    {
        LogHandleRequest(logger);
        return "Im here";
    }

    [LoggerMessage(LogLevel.Information, "ExampleHandler.HandleRequest was called")]
    public static partial void LogHandleRequest(ILogger logger);
}

