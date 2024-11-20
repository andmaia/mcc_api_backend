using Infrastructure.Context;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Configuration
{
    public class IntegrationFixture : IDisposable
    {
        public HttpClient httpClient { get; }
        public ApplicationDbContext DbContext { get; }
        public IServiceProvider serviceProvider { get; }
        private readonly IServiceScope _scope;

        public IntegrationFixture()
        {
            var api = new WebApplicationFactory();
            httpClient = api.CreateClient();
            _scope = api.Services.CreateScope();
            serviceProvider = _scope.ServiceProvider;
            DbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            DbContext.Database.EnsureCreated(); // Garante que o banco de dados de testes seja criado.

        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            _scope?.Dispose();
            httpClient.Dispose();
        }
    }

}
