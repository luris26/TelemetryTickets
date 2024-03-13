using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.PostgreSql;
using TicketsRUs.ClassLib.Data;

namespace TicketsRus.IntegrationTest
{
    public class TicketWebFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly PostgreSqlContainer _dbContainer;

        public TicketWebFactory()
        {
            var backupFile = Directory.GetFiles("../../../..", "*.sql", SearchOption.AllDirectories)
                .Select(f => new FileInfo(f))
                .OrderByDescending(fi => fi.LastWriteTime)
                .First();

            _dbContainer = new PostgreSqlBuilder()
              .WithImage("postgres")
              .WithPassword("Password@123")
              .WithBindMount(backupFile.FullName, "/docker-entrypoint-initdb.d/init.sql")
              .Build();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                //var connection = _dbContainer.GetConnectionString();
                services.RemoveAll(typeof(DbContextOptions<PostgresContext>));
                services.AddDbContextFactory<PostgresContext>(options => options.UseNpgsql(_dbContainer.GetConnectionString()));
            });
        }

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();
        }
        async Task IAsyncLifetime.DisposeAsync()
        {
            await _dbContainer.StopAsync();
        }
    }
}
