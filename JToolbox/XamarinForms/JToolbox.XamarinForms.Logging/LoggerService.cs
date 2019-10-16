﻿using JToolbox.Core.Abstraction;
using NLog;
using NLog.Config;
using System;

namespace JToolbox.XamarinForms.Logging
{
    public class LoggerService : ILoggerService
    {
        private readonly ILogger logger;
        private readonly string noMessage = "No message provided";

        public LoggerService()
        {
            logger = LogManager.GetCurrentClassLogger();
        }

        public LoggerService(string logPath) : this(new LoggerConfigBuilder(logPath).GetDefaultConfiguration())
        {
        }

        public LoggerService(LoggingConfiguration config) : this()
        {
            LogManager.Configuration = config;
        }

        public void Debug(string message, params object[] args)
        {
            logger.Debug(message, args);
        }

        public void Error(Exception exception)
        {
            logger.Error(exception, noMessage);
        }

        public void Error(Exception exception, string message, params object[] args)
        {
            logger.Error(exception, message, args);
        }

        public void Error(string message, params object[] args)
        {
            logger.Error(message, args);
        }

        public void Fatal(Exception exception)
        {
            logger.Fatal(exception, noMessage);
        }

        public void Fatal(Exception exception, string message, params object[] args)
        {
            logger.Fatal(exception, message, args);
        }

        public void Fatal(string message, params object[] args)
        {
            logger.Fatal(message, args);
        }

        public void Info(string message, params object[] args)
        {
            logger.Info(message, args);
        }

        public void Trace(string message, params object[] args)
        {
            logger.Trace(message, args);
        }

        public void Warn(string message, params object[] args)
        {
            logger.Warn(message, args);
        }
    }
}