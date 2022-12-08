using DataClasses;

namespace IntelligenceEngine.Test.FirstVersion;

public static class TestHelpers
{
    public static MediaToot[] GetMediaToots(int n, bool withDescription)
        =>
        Enumerable
        .Range(0, n)
        .Select( _ => new MediaToot(
            accountId: Guid.NewGuid().ToString(),
            accountName: Guid.NewGuid().ToString(),
            tootId: Guid.NewGuid().ToString(),
            hasAltText: withDescription,
            createdAt: DateTime.Now) 
        )
        .ToArray();
}
