using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DiscordSmashBot.ExceptionTypes
{
    public class BotConfigurationException : Exception
    {
        public BotConfigurationException()
        {
        }

        public BotConfigurationException(string message) : base(message)
        {
        }

        public BotConfigurationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
