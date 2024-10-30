// --------------------------------------------------------------------------------------------------
// <copyright file="CustomLogger.cs" company="LocalFriendz">
// Copyright (c) LocalFriendz. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Logging;

namespace LocalFriendzApi.Infrastructure.Logging
{
    public class CustomLogger : ILogger
    {
        public static bool Arquivo { get; set; } = false;

        private readonly string _loggerName;

        private readonly CustomLoggerProviderConfiguration _loggerConfig;

        public CustomLogger(
            string loggerName,
            CustomLoggerProviderConfiguration loggerConfig)
        {
            _loggerName = loggerName;
            _loggerConfig = loggerConfig;
        }

        public IDisposable? BeginScope<TState>(TState state)
            where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            string mensagem = string.Format($"{logLevel}: {eventId.Id} - {formatter(state, exception)}");
            if (Arquivo)
            {
                SaveLogger(mensagem);
            }

            Console.WriteLine(mensagem);

        }

        private void SaveLogger(string mensagem)
        {
            string caminhoArquivoLog = Environment.CurrentDirectory + @$"\LOG-{DateTime.Now:yyyy-MM-dd}.txt";

            if (!File.Exists(caminhoArquivoLog))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(caminhoArquivoLog));
                File.Create(caminhoArquivoLog).Dispose();
            }

            using StreamWriter streamWriter = new(caminhoArquivoLog, true);
            streamWriter.WriteLine(mensagem);
            streamWriter.Close();
        }
    }
}
