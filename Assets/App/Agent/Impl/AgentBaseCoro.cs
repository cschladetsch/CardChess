﻿using System.Collections;

using Flow;

namespace App.Agent
{
    using Model;

    /// <summary>
    /// Base for agents that perform actions over time.
    /// </summary>
    /// <typeparam name="TModel">The model that this Agent represents</typeparam>
    public abstract class AgentBaseCoro<TModel>
        : AgentBase<TModel>
        where TModel : class, IModel
    {
        protected AgentBaseCoro(TModel model)
            : base(model)
        {
        }

        public override void Create()
        {
            base.Create();
            //_Coro = New.Coroutine(Next);
            //_Node = New.Node(_Coro);
            //Kernel.Root.Add(_Node);
        }

        //protected abstract IEnumerator Next(IGenerator self);

        //protected IGenerator _Coro;

        protected INode _Node
        {
            get
            {
                if (_node != null)
                    return _node;
                _node = New.Node();
                Root.Add(_node);
                return _node;
            }
        }

        private INode _node;
    }
}
