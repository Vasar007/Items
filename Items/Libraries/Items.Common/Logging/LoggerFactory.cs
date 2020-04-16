using System;
using Acolyte.Assertions;

namespace Items.Common.Logging
{
    public static class LoggerFactory
    {
        /// <summary>
        /// Creates logger instance for passed type.
        /// </summary>
        /// <typeparam name="T">Type for which instance is created.</typeparam>
        /// <returns>Created logger instance.</returns>
        public static ILogger CreateLoggerFor<T>()
        {
            Type type = typeof(T);
            string loggerName = type.Name;
            return PrefixLogger.Create(loggerName);
        }

        /// <summary>
        /// Creates logger instance for passed class type.
        /// </summary>
        /// <param name="type">Class name. Try to pass it with <c>typeof</c> operator.</param>
        /// <returns>Created logger instance.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="type" /> is <c>null</c>.
        /// </exception>
        public static ILogger CreateLoggerFor(Type type)
        {
            type.ThrowIfNull(nameof(type));

            string loggerName = type.Name;
            return PrefixLogger.Create(loggerName);
        }
    }
}
