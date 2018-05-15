﻿namespace App
{
    public enum ELogLevel
    {
        None = 0,
        Info = 1,
        Warn = 2,
        Verbose = 4,
        Error = 8
    }

    public interface ILogger
    {
        string Prefix { get;set; }
        void Info(string fmt, params object[] args);
        void Warn(string fmt, params object[] args);
        void Error(string fmt, params object[] args);
    }

    public class LoggerFacade<TLogger> :
        ILogger where TLogger : class, ILogger, new()
    {
        public string Prefix { get { return _log.Prefix; } set { _log.Prefix = value; }}

        public LoggerFacade(string pre)
        {
            _log.Prefix = pre;
        }

        public void Info(string fmt, params object[] args)
        {
            _log.Info(fmt, args);
        }
        public void Warn(string fmt, params object[] args)
        {
            _log.Warn(fmt, args);
        }
        public void Error(string fmt, params object[] args)
        {
            _log.Error(fmt, args);
        }

        private TLogger _log = new TLogger();
    }
}

