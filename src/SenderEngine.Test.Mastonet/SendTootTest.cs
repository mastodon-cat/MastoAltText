using System.Reflection;
using DataClasses;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using SenderEngine.Mastonet;

namespace SenderEngine.Test.Mastonet;

public class SendTootTest
{
    [Theory]
    [InlineData(true)]
    public async Task ThisIsArealTootSender(bool useMock)
    {
        var services =
            new ServiceCollection()
            .AddSingleton(Mock.Of<ILogger<MastonetSender>>());

        if (useMock)
        {
            services.MockServices();
        }
        else
        {
            services.RealServices();
        }

        var provider =
            services
            .BuildServiceProvider();

        // StatusId: [109535551891158730] AccountName [ctrl_alt_d] AccountId: [109378184178969113]
        var sender = provider.GetRequiredService<ISender>();

        await sender.SendToot(
            "@ajuda, això hauria de ser un dm",
            true,
            "cat");
    }


}
