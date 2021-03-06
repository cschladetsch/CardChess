﻿namespace App.Agent
{
    using UniRx;
    using Dekuple.Agent;
    using Model;
    using Common;

    /// <summary>
    /// Acts on behalf of a piece.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class PieceAgent
        : AgentBase<IPieceModel>
        , IPieceAgent
    {
        public IReactiveProperty<Coord> Coord => Model.Coord;
        public EPieceType PieceType => Model.Card.Template.PieceType;
        public IReadOnlyReactiveProperty<bool> Dead => Model.Dead;
        public IReadOnlyReactiveProperty<int> ManaCost => Model.Power;
        public IReadOnlyReactiveProperty<int> Power => Model.Power;
        public IReadOnlyReactiveProperty<int> Health => Model.Health;

        public ECardType Type => Model.Card.Type;
        public ICardTemplate Template => Model.Card.Template;
        public IReactiveCollection<IItemModel> Items => Model.Card.Items;
        public IReactiveCollection<EAbility> Abilities { get; }
        public IReactiveCollection<IEffectModel> Effects { get; }

        public PieceAgent(IPieceModel model)
            : base(model)
        {
        }
    }
}
