﻿using System;
using Flow;

namespace App.Agent
{
    using Common;

    /// <summary>
    /// AgentBase for all agents. Provides a custom logger and an ITransient implementation
    /// to be used with Flow library.
    /// </summary>
    public class AgentLogger : ITransient, ILogger
    {
        public event TransientHandler Completed;
        public bool Active { get; private set; }
        public IKernel Kernel { get; set; }
        public string Name { get; set; }
        public IArbiterAgent Arbiter { get; set; }
        public Flow.IFactory New => Kernel.Factory;
        public Flow.INode Root => Kernel.Root;

        public string LogPrefix { get { return _log.LogPrefix; } set { _log.LogPrefix = value; }}
        public object Subject { get { return _log.Subject; } set { _log.Subject = value; } }
        public int Verbosity { get { return _log.Verbosity; } set { _log.Verbosity = value; }}

        protected AgentLogger()
        {
            _log.Subject = this;
            _log.LogPrefix = "Agent";
        }

        public ITransient Named(string name)
        {
            Name = name;
            return this;
        }

        public void Complete()
        {
            if (!Active)
                return;
            Completed?.Invoke(this);
            Active = false;
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

        public void Verbose(int level, string fmt, params object[] args)
        {
            _log.Verbose(level, fmt, args);
        }

        protected readonly LoggerFacade<Flow.Impl.Logger> _log = new LoggerFacade<Flow.Impl.Logger>("Agent");
    }

    public abstract class AgentLogger<TModel>
        : AgentLogger
        where TModel : Model.IModel
    {
        public Model.IModel BaseModel { get; private set; }
        public TModel Model { get; private set; }
        public IOwner Owner { get; private set; }

        public bool Construct(TModel a0, IOwner owner)
        {
            Model = a0;
            BaseModel = a0;
            Owner = owner;
            return true;
        }
    }

}
