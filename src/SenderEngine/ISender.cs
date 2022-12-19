namespace SenderEngine;
public interface ISender : IDisposable
{
    Task SendToot(string body, bool isPublicToot, string language);
}
