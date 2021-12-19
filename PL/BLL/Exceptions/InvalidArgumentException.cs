using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Exceptions
{
    internal class InvalidArgumentException : Exception
    {
        public readonly string Massege = "Provided object data is invalid";
    }
}
