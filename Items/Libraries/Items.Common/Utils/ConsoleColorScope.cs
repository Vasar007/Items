using System;
using Acolyte.Assertions;

namespace Items.Common.Utils
{
    public sealed class ConsoleColorScope : IDisposable
    {
        private readonly ConsoleColor _resetColor;

        public ConsoleColorScope(
            ConsoleColor consoleColor,
            ConsoleColor resetColor = ConsoleColor.White)
        {
            _resetColor = resetColor.ThrowIfEnumValueIsUndefined(nameof(resetColor));
            Console.ForegroundColor = consoleColor.ThrowIfEnumValueIsUndefined(nameof(consoleColor));
        }

        #region IDisposable Members

        private bool _disposed;

        public void Dispose()
        {
            if (_disposed) return;

            Console.ForegroundColor = _resetColor;

            _disposed = true;
        }

        #endregion
    }
}
