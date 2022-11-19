using System;

namespace RedRift.Common.Exceptions
{
    public abstract class UserException : Exception
    {
        // TODO - localization

        public UserException(string message)
            : base(message)
        {

        }
    }
}
