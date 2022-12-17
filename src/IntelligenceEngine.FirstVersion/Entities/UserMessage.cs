using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligenceEngine.FirstVersion.Entities
{
    internal record UserMessage : IUserMessage
    {
        public MessageType MessageType { get; init; }

        public string? Message { get; init; }

        public string? PublicMessage { get; init; }
    }
}
