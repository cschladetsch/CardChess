﻿using Flow;

namespace App.Agent
{
    using Model;

    /// <summary>
    /// Base for agents that perform actions over time.
    /// </summary>
    /// <typeparam name="TModel">The model that this Agent represents</typeparam>
    public abstract class AgentBaseCoro<TModel>
        : Dekuple.Agent.AgentBase<IAgent, TModel>
        where TModel 
            : class
            , IModel
    {
        public IArbiterAgent Arbiter { get;set;}
        public IBoardAgent Board { get;set;}

        protected AgentBaseCoro(TModel model)
            : base(model)
        {
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
                _node.Name = Name;
                Root.Add(_node);
                return _node;
            }
        }

        private INode _node;
    }
}
