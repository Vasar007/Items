using System;

namespace Items.Common.Logging
{
    public interface ILogger
    {
        void Debug(string message);

        void Message(string message);

        void Warning(string message);

        void Error(string message);

        void Exception(Exception ex, string? message = null);

        void SkipLine();
    }
}
