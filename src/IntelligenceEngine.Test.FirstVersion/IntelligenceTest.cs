using Microsoft.Extensions.DependencyInjection;
using IntelligenceEngine.FirstVersion;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Text;
using DataClasses;
using FluentAssertions;

namespace IntelligenceEngine.Test.FirstVersion;

public class IntelligenceSimpleTest
{
    private readonly ServiceProvider ServiceProvider;
    public IntelligenceSimpleTest()
    {
        var jsonconfstring = @"{
            ""Toots"": {
                ""cat"": {
                    ""FirstNonDescriptionToot"":""T1"",
                    ""SecondNonDescriptionToot"":""T2"",
                    ""ThirdNonDescriptionToot"":""T3"",
                    ""ConsecutiveNonDescriptionTootMultiple50"":""T4"",
                    ""NoCase"":""""      
                }
            }        
        }";

        var configuration = 
            new ConfigurationBuilder()
            .AddJsonStream(new MemoryStream(Encoding.ASCII.GetBytes(jsonconfstring)))
            .Build();

        var services = 
            new ServiceCollection()
            .AddSingleton<IConfiguration>(configuration)
            .AddSingleton<Intelligence>();

        ServiceProvider = 
            services
            .BuildServiceProvider();
    }

    [Fact]
    public void FirstNonDescriptionTootTest()
    {
        // -- ARRANGE --
        var intelligence = ServiceProvider.GetRequiredService<Intelligence>();

        var data = new [] {new MediaToot("", "", "", false, DateTime.Now) };

        // -- ACT --
        var status = intelligence.GetCase(data);

        // -- ASSERT --
        status.Should().Be(Intelligence.CaseEnum.FirstNonDescriptionToot);
    }

    [Fact]
    public void AbleToGetTextFromConfiguration()
    {
        // -- ARRANGE --
        var intelligence = ServiceProvider.GetRequiredService<Intelligence>();


        // -- ACT --
        var msg = intelligence.GetTootFromCase(Intelligence.CaseEnum.FirstNonDescriptionToot);

        // -- ASSERT --
        msg.Should().Be("T1");
    }
}