using IntelligenceEngine.FirstVersion;
using Microsoft.Extensions.Options;
using IntelligenceEngine.FirstVersion.Entities;

namespace IntelligenceEngine.Test.FirstVersion
{
    public class IntelligenceTest
	{

		[Fact]
		public async Task GetMessage_ReturnsSecondMessage_WhenTotalTootsEquals3()
		{
			var list = new List<AppMessage> {
				new AppMessage
				{
					Conditions = new List<AppMessage.Condition>
					{
						new AppMessage.Condition("TotalToots", "==", "1")
					},
					MessageType = MessageType.Warn,
					Message = "first",
				},
				new AppMessage
				{
					Conditions = new List<AppMessage.Condition>
					{
						new AppMessage.Condition("TotalToots", "==", "3")
					},
					MessageType = MessageType.Warn,
					Message = "second",
				}
			};
			var listWrapped = Options.Create(list);
			var store = TestHelpers.CreateMockStore(3, 0, 0, 3);
			var intelligence = new IntelligenceEngine.FirstVersion.Intelligence(listWrapped, store.Object);
			var message = await intelligence.GetMessage("abcde");
			Assert.Equal("second", message.Message);
		}

		
		[Fact]
		public async Task GetMessage_ReturnsSecondMessage_WhenTotalTootsEquals3_AndConsecutiveWithDescriptionsEquals2()
		{
			var list = new List<AppMessage> {
				new AppMessage
				{
					Conditions = new List<AppMessage.Condition>
					{
						new AppMessage.Condition("TotalToots", "==", "3"),
						new AppMessage.Condition("LastConsecutivesWithDescription ", "==", "0")
					},
					MessageType = MessageType.Warn,
					Message = "first",
				},
				new AppMessage
				{
					Conditions = new List<AppMessage.Condition>
					{
						new AppMessage.Condition("TotalToots", "==", "3"),
						new AppMessage.Condition("LastConsecutivesWithDescription ", "==", "2")
					},
					MessageType = MessageType.Warn,
					Message = "second",
				}
			};
			
			var listWrapped = Options.Create(list);
			var store = TestHelpers.CreateMockStore(3, 2, 2, 1);

			var intelligence = new Intelligence(listWrapped, store.Object);
			var message = await intelligence.GetMessage("abcde");
			Assert.Equal("second", message.Message);
		}

	}
}
