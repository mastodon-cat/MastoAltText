using DataClasses;
using Microsoft.EntityFrameworkCore;

namespace StoreEngine.DbContext;

public class MediaTootModel
{
    public int Id { get; set; }
    public int UserSequenceNumber { get; set; }
    public string AccountId { get; set; } = default!;
    public string AccountName { get; set; } = default!;
    public string TootId { get; set; } = default!;
    public bool HasAltText { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = default!;
}