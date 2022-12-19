using System.Reflection;
using DataClasses;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using SenderEngine.Mastonet;

namespace SenderEngine.Test.Mastonet;

public class UnitTest1
{
    [Theory]
    [InlineData(false)]
    public async Task ThisIsArealTootSender(bool confirm)
    {
        var config = 
            new ConfigurationBuilder()
            .AddUserSecrets(Assembly.GetExecutingAssembly(), false)
            .Build();

        var services = 
            new ServiceCollection()
            .Configure<MastodonParms>(config.GetSection(nameof(MastodonParms)))
            .AddSingleton<ISender, MastonetSender>()
            .AddSingleton<ILogger<MastonetSender>>(Mock.Of<ILogger<MastonetSender>>())
            .BuildServiceProvider();

        // StatusId: [109535551891158730] AccountName [ctrl_alt_d] AccountId: [109378184178969113]
        var sender = services.GetRequiredService<ISender>();
        var u = 1;
        if (confirm)
        {
            await sender.SendToot(
                "@ajuda, això hauria de ser un dm", 
                true, 
                "cat");
        }
    }
}