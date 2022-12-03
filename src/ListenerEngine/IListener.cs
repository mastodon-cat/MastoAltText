namespace ListenerEngine;

 public class ListenerEventArgs : EventArgs
 {
    public ListenerEventArgs(string user, string tootId, bool hasAltText)
    {
        User = user;
        TootId = tootId;
        HasAltText = hasAltText;
    }

    public string User {get;}
    public string TootId {get;}
    public bool HasAltText {get;}
 }
public interface IListener
{   
    event EventHandler<ListenerEventArgs>? NewMediaToot; 
    void Start();
    void Stop();
}
