using System;
using System.Collections.Generic;
using System.Text;

namespace core.Utilities.Results
{
    public class ErorrResult : Result
    {
        public ErorrResult(string message) : base(false, message)
        {
        }

        public ErorrResult() : base(false)
        {
        }
    }
}
