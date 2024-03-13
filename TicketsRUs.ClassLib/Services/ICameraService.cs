namespace TicketsRUs.ClassLib.Services;

public interface ICameraService
{
    Task<string> GetScanResultsAsync();
}
