using System;
using System.Collections.Generic;
using System.Text;

namespace core.Utilities.Results
{
    public interface IResult
    {
        bool success { get; }
        string Message { get; }
    }
}
