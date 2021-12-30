using System;

namespace Administration.Exceptions
{
    public class InvalidArgumentException : Exception
    {
        public readonly string Massege = "Provided object data is invalid";
    }
}
