using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligenceEngine;

public interface IMediaDescriptionUserState
{
    public int TotalToots { get; }
    public int TootsWithDescription { get; }
    public int PercentajeTootsWithDescription { get; }
}
