using DataClasses;

namespace StoreEngine;
public interface IStore: IDisposable
{
    Task SaveMediaTootAsync(MediaToot mediaToot);
    Task<List<MediaToot>> GetMediaTootsByUserIdAsync(string userId, int? year);

}
