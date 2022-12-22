namespace ListenerEngine;
public interface IListener : IDisposable
{   
    event EventHandler<ListenerEventArgs>? NewMediaToot; 
    Task Start();
}
