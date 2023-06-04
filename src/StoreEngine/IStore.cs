using DataClasses;

namespace StoreEngine;
public interface IStore : IDisposable
{
    /// <summary>
    /// Saves the media toot asynchronous.
    /// </summary>
    /// <param name="mediaToot">The media toot.</param>
    /// <returns></returns>
    Task SaveMediaTootAsync(MediaToot mediaToot);

    /// <summary>
    /// Gets the media toots by user identifier asynchronous.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="year">The year.</param>
    /// <returns></returns>
    Task<List<MediaToot>> GetMediaTootsByUserIdAsync(string userId, int? year);

    /// <summary>
    /// Gets the toot count by user identifier asynchronous.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="withDescription">The with description.</param>
    /// <returns></returns>
    Task<int> GetTootCountByUserIdAsync(string userId, bool? withDescription = null);

    /// <summary>
    /// Gets the number of consecutive toots with description by user identifier asynchronous.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <returns></returns>
    Task<int> GetNumberOfConsecutiveTootsWithDescriptionByUserIdAsync(string userId);

    /// <summary>
    /// Gets the number of consecutive toots without description by user identifier asynchronous.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <returns></returns>
    Task<int> GetNumberOfConsecutiveTootsWithoutDescriptionByUserIdAsync(string userId);
}
