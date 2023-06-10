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
