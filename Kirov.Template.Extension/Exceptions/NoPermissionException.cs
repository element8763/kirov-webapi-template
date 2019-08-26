using System;

namespace Extension.Template.Exceptions
{
    public class NoPermissionException : ApplicationException
    {

        public NoPermissionException() { }

        public NoPermissionException(string message) : base(message)
        {

        }
    }
}
