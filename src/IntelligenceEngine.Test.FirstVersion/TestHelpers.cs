using DataClasses;

using Moq;

using StoreEngine;

namespace IntelligenceEngine.Test.FirstVersion;

public static class TestHelpers
{

	internal static Mock<IStore> CreateMockStore(int totalTootCount, int tootCountWithDescriptions, int numberOfConsecutiveTootsWithDescription, int numberOfConsecutiveTootsWithoutDescription)
	{
		var mock = new Mock<IStore>();
		mock.Setup(m => m.GetTootCountByUserIdAsync(It.IsAny<string>(), null))
			.ReturnsAsync(totalTootCount);
		mock.Setup(m => m.GetTootCountByUserIdAsync(It.IsAny<string>(), true))
			.ReturnsAsync(tootCountWithDescriptions);
		mock.Setup(m => m.GetNumberOfConsecutiveTootsWithDescriptionByUserIdAsync(It.IsAny<string>()))
			.ReturnsAsync(numberOfConsecutiveTootsWithDescription);
		mock.Setup(m => m.GetNumberOfConsecutiveTootsWithoutDescriptionByUserIdAsync(It.IsAny<string>()))
			.ReturnsAsync(numberOfConsecutiveTootsWithoutDescription);
		return mock;
	}
}
