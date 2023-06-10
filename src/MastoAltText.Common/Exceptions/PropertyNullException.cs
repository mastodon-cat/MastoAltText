using MastoAltText.Common.Exceptions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastoAltText.Common.Exceptions;

public class PropertyNullException : ArgumentException
{
    public PropertyNullException(string paramName, string propertyName)
        : base($"{propertyName} property in {paramName} cannot be null.", paramName)
    {
    }
}