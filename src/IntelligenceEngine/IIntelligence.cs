namespace IntelligenceEngine;
public interface IIntelligence
{
    Task<IUserMessage?> GetMessage(string userId);
}