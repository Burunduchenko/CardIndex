using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Exceptions
{
    public class AlreadyExistException : Exception
    {
        public readonly string Massage = "You try to add existed object";
    }
}
