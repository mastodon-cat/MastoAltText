namespace DataClasses;
public class MediaToot
{
    public MediaToot(int? sequenceNumber, string accountId, string accountName, string tootId, bool hasAltText, DateTime createdAt)
    {
        SequenceNumber = sequenceNumber;
        AccountId = accountId;
        AccountName = accountName;
        TootId = tootId;
        HasAltText = hasAltText;
        CreatedAt = createdAt;
    }

    public int? SequenceNumber { get; set; }
    public string AccountId { get; }
    public string AccountName { get; }
    public string TootId { get; }
    public bool HasAltText { get; }
    public DateTime CreatedAt { get; }
}
