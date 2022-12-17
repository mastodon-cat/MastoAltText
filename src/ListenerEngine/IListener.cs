namespace ListenerEngine;
public interface IListener : IDisposable
{   
    event EventHandler<ListenerEventArgs>? NewMediaToot; 
    void Start();
}
