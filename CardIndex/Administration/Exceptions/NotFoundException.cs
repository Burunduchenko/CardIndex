using System;

namespace Administration.Exceptions
{
    public class NotFoundException : Exception
    {
        public readonly string Massege = "Provided object not found";
    }
}
