namespace DataClasses;
public class MediaToot
{
    public MediaToot(string accountId, string accountName, string tootId, bool hasAltText, DateTime createdAt)
    {
        AccountId = accountId;
        AccountName = accountName;
        TootId = tootId;
        HasAltText = hasAltText;
        CreatedAt = createdAt;
    }

    public string AccountId {get;  }
    public string AccountName {get; }
    public string TootId {get; }
    public bool HasAltText {get;  }
    public DateTime CreatedAt { get; } 
}
