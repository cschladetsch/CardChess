namespace App.Agent
{
    using System;
    using Flow;
    using UniRx;
    using Dekuple;
    using Dekuple.Agent;
    using Model;
    using Common;
    using Common.Message;

    /// <inheritdoc cref="IAgent{TModel}" />
    /// <summary>
    /// Agent for a Player.
    /// Responsible for reacting to changes in Model state.
    /// </summary>
    public interface IPlayerAgent
        : IAgent<IPlayerModel>
        , IOwner
    {
        IDeckAgent Deck { get; }
        IHandAgent Hand { get; }
        IBoardAgent Board { get; }
        IEndTurnButtonAgent EndTurnButton { get; }
        EColor Color { get; }

        IReadOnlyReactiveProperty<int> MaxMana { get; }
        IReadOnlyReactiveProperty<int> Mana { get; }
        IReadOnlyReactiveProperty<int> Health { get; }

        IFuture<RejectCards> Mulligan();

        ITransient TurnStart();
        ITimedFuture<Turnaround> NextRequest(float timeOut);
        ITransient TurnEnd();

        void PushRequest(IRequest request, Action<IResponse> handler);
    }
}
