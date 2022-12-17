using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligenceEngine.FirstVersion.Entities;

public record AppMessage
{
    public record Condition(string Field, string Operator, string Value);
    public List<Condition> Conditions { get; init; } = new List<Condition>();
    public string? Message { get; init; }
    public MessageType MessageType { get; init; }
    public string? PublicMessage { get; init; }
}