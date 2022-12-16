namespace ListenerEngine;
public interface IListener
{   
    event EventHandler<ListenerEventArgs>? NewMediaToot; 
    void Start();
    void Stop();
}
