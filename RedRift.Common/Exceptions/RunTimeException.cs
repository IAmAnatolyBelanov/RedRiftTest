using System;

namespace RedRift.Common.Exceptions
{
    public abstract class RunTimeException : Exception
    {
        // TODO - localization

        public RunTimeException(string message)
            : base(message)
        {

        }
    }
}
