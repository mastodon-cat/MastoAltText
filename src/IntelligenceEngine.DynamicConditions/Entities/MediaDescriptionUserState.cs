using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligenceEngine.DynamicConditions.Entities;

internal record MediaDescriptionUserState : IMediaDescriptionUserState
{
    private int? percentaje;

    public int TotalToots { get; init; }
    public int TootsWithDescription { get; init; }
    public int LastConsecutivesWithDescription { get; init; }
    public int LastConsecutivesWithoutDescription { get; init; }

    public int PercentajeTootsWithDescription => percentaje ??= TootsWithDescription * 100 / TotalToots;
}
