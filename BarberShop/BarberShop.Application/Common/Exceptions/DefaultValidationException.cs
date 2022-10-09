using System;

namespace BarberShop.Application.Common.Exceptions
{
    public class DefaultValidationException : Exception
    {
        public DefaultValidationException(string msg) : base(msg) { }
    }
}
