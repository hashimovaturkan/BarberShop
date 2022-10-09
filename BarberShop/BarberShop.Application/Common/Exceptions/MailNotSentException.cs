using System;

namespace BarberShop.Application.Common.Exceptions
{
    public class MailNotSentException : Exception
    {
        public MailNotSentException(string additionalMessage = null)
            : base($"A mail has not been sent." + (string.IsNullOrWhiteSpace(additionalMessage) ? "" : (" " + additionalMessage))) { }
    }
}
