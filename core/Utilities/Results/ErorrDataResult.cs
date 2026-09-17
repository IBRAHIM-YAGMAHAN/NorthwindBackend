using System;
using System.Collections.Generic;
using System.Text;

namespace core.Utilities.Results
{
    public class ErorrDataResult<T> : DataResult<T>
    {
        public ErorrDataResult(T data,string message) : base(data, false, message)
        {
        }

        public ErorrDataResult(T data) : base(data, false)
        {
        }

        public ErorrDataResult(string message) : base(default, false, message)
        {
        }

        public ErorrDataResult() : base(default, false)
        {
        }
    }
}
