using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Exceptions
{
    public class NotFoundException : Exception
    {
        public readonly string Massege = "Provided object not found";
    }
}
