using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListenerEngine.Mastonet.Config;

public record MastonetConfig
{
	public string? Instance { get; init; }
	public string? AccessToken { get; init; }
}
