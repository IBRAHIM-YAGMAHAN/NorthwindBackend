using System;
using System.Collections.Generic;
using System.Text;

namespace core.Utilities.Results
{
    public class Result : IResult
    {
        public Result(bool success, string message) : this(success)
        {
            Message = message;
        }
        public Result(bool success)
        {
            this.success = success;
        }
        public bool success { get; }
        public string Message { get; }

    }
}
