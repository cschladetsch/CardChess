﻿using System.Linq;
using UniRx;
using UnityEngine.UI;

namespace App.Model.Impl
{
    using Common;
    using Model;
    using Registry;

    public class EndTurnButtonModel
        : ModelBase
        , IEndTurnButtonModel
    {
        public IReadOnlyReactiveProperty<bool> Interactive => _isInteractive;
        public IReadOnlyReactiveProperty<bool> PlayerHasOptions => _playerHasOptions;

        [Inject] public IBoardModel _board;
        [Inject] public IArbiterModel _arbiter;

        public EndTurnButtonModel(IOwner owner) : base(owner) { }

        public override void PrepareModels()
        {
            base.PrepareModels();

            _arbiter.CurrentPlayer.Subscribe(p => _isInteractive.Value = p == PlayerModel);

            _arbiter.LastResponse.Subscribe(resp =>
            {
                var mana = PlayerModel.Mana.Value;
                var canPlace = PlayerModel.Hand.Cards.Any(c => c.ManaCost.Value <= mana);
                var canMove = mana > 1 && _board.Pieces.Where(SameOwner).Any(_board.CanMoveOrAttack);
                _playerHasOptions.Value = canPlace || canMove;
                Info($"CanMove={canMove}, canPlace={canPlace}, mana={mana}, {PlayerModel}: hasOptions={_playerHasOptions.Value}");
            });//.AddTo(this);
        }

        private ReactiveProperty<int> _canPlay;
        private ReactiveProperty<int> _canMove;

        private readonly BoolReactiveProperty _isInteractive = new BoolReactiveProperty();
        private readonly BoolReactiveProperty _playerHasOptions = new BoolReactiveProperty();
    }
}
