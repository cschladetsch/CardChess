﻿namespace App.Agent
{
    using UniRx;
    using Dekuple.Agent;
    using Model;

    /// <inheritdoc cref="AgentBaseCoro{TModel}" />
    /// <summary>
    /// Representative for the Hand owned by the player.
    /// </summary>
    public class HandAgent
        : AgentBaseCoro<IHandModel>
        , IHandAgent
    {
        public IReadOnlyReactiveCollection<ICardAgent> Cards => _cards;
        private readonly ReactiveCollection<ICardAgent> _cards = new ReactiveCollection<ICardAgent>();

        public HandAgent(IHandModel model)
            : base(model)
        {
        }

        public override void StartGame()
        {
            Model.StartGame();
            Verbose(10, $"HandAgent {PlayerModel} created");
            foreach (var c in Model.Cards)
                _cards.Add(Registry.New<ICardAgent>(c));

            Model.Cards.ObserveAdd().Subscribe(Add);//.AddTo(this);
            Model.Cards.ObserveRemove().Subscribe(Remove);
        }

        private void Remove(CollectionRemoveEvent<ICardModel> remove)
        {
            Verbose(10, $"HandAgent: Remove {remove.Value} @{remove.Index}");
            var index = remove.Index;
            _cards.RemoveAt(index);
        }

        private void Add(CollectionAddEvent<ICardModel> add)
        {
            Verbose(10, $"HandAgent: Add {add.Value} @{add.Index}");
            _cards.Insert(add.Index, Registry.New<ICardAgent>(add.Value));
        }
    }
}
