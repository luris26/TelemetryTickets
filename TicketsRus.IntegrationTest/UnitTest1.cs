using Microsoft.AspNetCore.Mvc.Testing;

namespace TicketsRus.IntegrationTest
{
    public class UnitTest1 : IClassFixture<TicketWebFactory>
    {
        public UnitTest1(TicketWebFactory factory)
        {
            factory.CreateDefaultClient();
        }
        [Fact]
        public void Test1()
        {
            Assert.True(true);
        }
    }
}
