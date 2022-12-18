using DataClasses;

namespace StoreEngine;
public interface IStore : IDisposable
{
	Task SaveMediaTootAsync(MediaToot mediaToot);
	Task<List<MediaToot>> GetMediaTootsByUserIdAsync(string userId, int? year);
	Task<int> GetTootCountByUserIdAsync(string userId, bool? withDescription = null);
	Task<int> GetNumberOfConsecutiveTootsWithDescriptionByUserIdAsync(string userId);
	Task<int> GetNumberOfConsecutiveTootsWithoutDescriptionByUserIdAsync(string userId);
}
