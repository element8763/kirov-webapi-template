using System;

namespace Extension.Template.Exceptions
{
    public class BusinessException : ApplicationException
    {

        public BusinessException() { }

        public BusinessException(string message) : base(message)
        {

        }
    }
}
