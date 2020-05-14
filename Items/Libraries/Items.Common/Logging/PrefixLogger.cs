using System;
using Acolyte.Assertions;
using Items.Common.Utils;

namespace Items.Common.Logging
{
    internal sealed class PrefixLogger : ILogger
    {
        private readonly string _prefix;


        private PrefixLogger(
            string prefix)
        {
            _prefix = prefix.ThrowIfNullOrEmpty(nameof(prefix));
        }

        public static PrefixLogger Create(
            string prefix)
        {
            return new PrefixLogger(WrapPrefix(prefix));
        }

        #region ILogger Implementation

        public void Debug(string message)
        {
            message.ThrowIfNull(nameof(message));

            using var colorScope = new ConsoleColorScope(ConsoleColor.Cyan);

            Console.Out.WriteLine($"Debug: {_prefix} {message}");
        }

        public void Message(string message)
        {
            message.ThrowIfNull(nameof(message));

            using var colorScope = new ConsoleColorScope(ConsoleColor.Green);

            Console.Out.WriteLine($"Message: {_prefix} {message}");
        }

        public void Warning(string message)
        {
            message.ThrowIfNull(nameof(message));

            using var colorScope = new ConsoleColorScope(ConsoleColor.Yellow);

            Console.Out.WriteLine($"Warning: {_prefix} {message}");
        }

        public void Error(string message)
        {
            message.ThrowIfNull(nameof(message));

            using var colorScope = new ConsoleColorScope(ConsoleColor.Red);

            Console.Error.WriteLine($"Error: {_prefix} {message}");
        }

        public void Exception(Exception ex, string? message = null)
        {
            using var colorScope = new ConsoleColorScope(ConsoleColor.Red);

            if (message != null)
            {
                Console.Error.WriteLine($"Exception: {_prefix} {message}");
            }

            Console.Error.WriteLine($"Exception: {_prefix} {ex}");
        }

        public void SkipLine()
        {
            Console.Out.WriteLine();
        }

        #endregion

        private static string WrapPrefix(string prefix)
        {
            return $"[{prefix}]";
        }
    }
}
