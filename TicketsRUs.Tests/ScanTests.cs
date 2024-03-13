using Moq;
using TicketsRUs.ClassLib.Data;
using TicketsRUs.ClassLib.Services;
using TicketsRUs.ClassLib;
// using TicketsRUs.ClassLib.Data;
// using TicketsRUs.ClassLib.Services;

namespace TicketsRUs.Tests;

public class ScanTests
{
    //This was a previous test
    [Fact]
    public async void UnsuccessfulScan_DoesNotUpdateDatabasce()
    {
        //ARRANGE
        Ticket t = new Ticket()
        {
            Id = 0,
            EventId = 0,
            Scanned = false,
            Identifier = "123456789"
        };
        //New instamce
        Ticket wrongTicket = new Ticket()
        {
            Id = 0,
            EventId = 1,
            Scanned = false,
            Identifier = "123456789"
        };

        Mock<ITicketService> mockService = new();
        mockService.Setup(m => m.GetTicket(t.Identifier))
            .Returns(Task.FromResult(t));
        mockService.Setup(m => m.GetTicket(wrongTicket.Identifier))
            .Returns(Task.FromResult(wrongTicket));
        mockService.Setup(m => m.UpdateTicket(It.IsAny<Ticket>()))
            .Callback(() => t.Scanned = true);

        Mock<ICameraService> mockCamera = new();

        mockCamera.Setup(m => m.GetScanResultsAsync())
            .Returns(Task.FromResult(wrongTicket.Identifier));

        QRScanner scanner = new(mockService.Object, mockCamera.Object);


        //ACT in the test
        await scanner.DoScanAsync(0);

        //ASSERT
        mockService.Verify(m => m.GetTicket(wrongTicket.Identifier));
        mockService.Verify(m => m.UpdateTicket(It.IsAny<Ticket>()), Times.Never());
        mockCamera.Verify(m => m.GetScanResultsAsync());

        Assert.False(t.Scanned);
        Assert.False(wrongTicket.Scanned);
    }
}

internal interface ICameraController
{
}