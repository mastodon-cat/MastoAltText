using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligenceEngine
{
	public interface IUserMessage
	{
		MessageType MessageType { get; }
		string Message { get; }
		string PublicMessage { get; }
	}
}
