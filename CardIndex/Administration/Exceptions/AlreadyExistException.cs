using System;

namespace Administration.Exceptions
{
    public class AlreadyExistException : Exception
    {
        public readonly string Massage = "You try to add existed object";
    }
}
