namespace ListenerEngine;

 public class ListenerEventArgs : EventArgs
 {
    public ListenerEventArgs(string accountId, string accountName, string tootId, bool hasAltText)
    {
        AccountId = accountId;
        AccountName = accountName;
        TootId = tootId;
        HasAltText = hasAltText;
    }

    public string AccountId {get;}
    public string AccountName {get;}
    public string TootId {get;}
    public bool HasAltText {get;}
 }
public interface IListener
{   
    event EventHandler<ListenerEventArgs>? NewMediaToot; 
    void Start();
    void Stop();
}
