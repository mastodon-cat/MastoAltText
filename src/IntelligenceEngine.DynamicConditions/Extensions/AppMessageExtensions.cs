using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using IntelligenceEngine.DynamicConditions.Entities;

namespace IntelligenceEngine.DynamicConditions.Extensions
{
    internal static class AppMessageExtensions
    {
        internal static IUserMessage ToUserMessage(this AppMessage message) =>
            new UserMessage { Message = message.Message, MessageType = message.MessageType, PublicMessage = message.PublicMessage };
    }
}
