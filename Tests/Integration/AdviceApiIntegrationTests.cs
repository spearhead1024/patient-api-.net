using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using PatientAdviceAPI.Interfaces;
using Xunit;

namespace PatientAdviceAPI.Tests.Integration
{
    public class AdviceApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public AdviceApiIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory
                .WithWebHostBuilder(builder =>
                {
                    var projectDir = Directory
                        .GetParent(AppContext.BaseDirectory)  // ...\bin\Debug\net9.0
                        .Parent                                 // ...\bin\Debug
                        .Parent                                 // ...\bin
                        .Parent                                 // ...\PatientAdviceAPI
                        .FullName;
                    builder.UseContentRoot(projectDir);

                    builder.ConfigureServices(services =>
                    {
                        var llmMock = new Mock<ILLMService>();
                        llmMock
                            .Setup(s => s.GetAdviceAsync(It.IsAny<string>()))
                            .ReturnsAsync("IntegrationTest Advice");
                        services.AddSingleton(llmMock.Object);
                    });
                })
                .CreateClient();
        }

        [Fact]
        public async Task GetAdvice_ReturnsOk_WithExpectedAdvice()
        {
            var resp = await _client.GetAsync("/api/v1/patients/test-id/advice");
            Assert.Equal(HttpStatusCode.OK, resp.StatusCode);

            var json = await resp.Content.ReadAsStringAsync();
            Assert.Contains("IntegrationTest Advice", json);
        }
    }
}
