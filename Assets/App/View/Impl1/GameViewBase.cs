﻿namespace App.View.Impl1
{
    using Dekuple;
    using Dekuple.Agent;
    using Dekuple.View.Impl;
    using Agent;

    /// <summary>
    /// Common for many game object views.
    /// </summary>
    public class GameViewBase
        : ViewBase
        , IGameViewBase
    {
        public IPlayerAgent PlayerAgent => Owner.Value as IPlayerAgent;
        [Inject] public IArbiterView ArbiterView { get; set; }
        [Inject] public IBoardView BoardView { get; set; }

        protected bool IsCurrentPlayer() => ArbiterView.CurrentPlayerOwns(this);
    }

    public class GameViewBase<TAgent>
        : GameViewBase
        where TAgent
            : class
            , IAgent
    {
        public TAgent Agent => AgentBase as TAgent;
    }
}
