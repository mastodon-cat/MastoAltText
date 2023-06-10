using DataClasses;
using IntelligenceEngine.DynamicConditions.Entities;
using IntelligenceEngine.DynamicConditions.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

using StoreEngine;

using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace IntelligenceEngine.DynamicConditions;

public class Intelligence : IIntelligence
{
    private readonly List<AppMessage> messages;
    private readonly IStore store;

    public Intelligence(IOptions<List<AppMessage>> messages, IStore store)
    {
        this.messages = messages?.Value ?? throw new ArgumentNullException(nameof(messages));
        this.store = store ?? throw new ArgumentNullException(nameof(store));
    }

    public async Task<IUserMessage?> GetMessage(string userId)
    {
        var userState = await GetUserState(userId);
        return messages.FirstOrDefault(m => MessageMatchWithUserState(m, userState))?.ToUserMessage();
    }

    private static bool MessageMatchWithUserState(AppMessage message, MediaDescriptionUserState userState)
    {
        List<string> conditions = new();
        foreach (var condition in message.Conditions)
        {
            conditions.Add($"({condition.Field} {condition.Operator} {condition.Value})");
        }
        var expression = string.Join(" and ", conditions);
        return new List<MediaDescriptionUserState>() { userState }.AsQueryable().Where(expression).Any();
    }

    private async Task<MediaDescriptionUserState> GetUserState(string userId)
    {
        return new MediaDescriptionUserState
        {
            TotalToots = await store.GetTootCountByUserIdAsync(userId),
            TootsWithDescription = await store.GetTootCountByUserIdAsync(userId, true),
            LastConsecutivesWithDescription = await store.GetNumberOfConsecutiveTootsWithDescriptionByUserIdAsync(userId),
            LastConsecutivesWithoutDescription = await store.GetNumberOfConsecutiveTootsWithoutDescriptionByUserIdAsync(userId)
        };
    }
}
