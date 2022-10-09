using System;

namespace BarberShop.Application.Common.Exceptions
{
    public class ArgumentCustomException : ArgumentException
    {
        public ArgumentCustomException(string name, object key)
            : base($"Input \"{name}\" can't have ({key}) value.") {}
    }
}
