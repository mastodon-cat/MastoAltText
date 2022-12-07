using DataClasses;

namespace ListenerEngine;

 public class ListenerEventArgs : EventArgs
 {
    public ListenerEventArgs(MediaToot mediaToot)
    {
        MediaToot = mediaToot;
    }

    public MediaToot MediaToot { get; }
}
public interface IListener
{   
    event EventHandler<ListenerEventArgs>? NewMediaToot; 
    void Start();
    void Stop();
}
