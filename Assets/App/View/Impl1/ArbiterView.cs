﻿using App.Common.Message;
using UnityEngine.UI;
using UniRx;

namespace App.View.Impl1
{
    using Agent;
    using Common;

    public class ArbiterView
        : ViewBase<IArbiterAgent>
        , IArbiterView
    {
        public BoardView Board;
        public PlayerView WhitePlayer;
        public PlayerView BlackPlayer;
        public TMPro.TextMeshPro CurrentPlayerText;
        public TMPro.TextMeshPro ResponseText;
        public TMPro.TextMeshPro StateText;
        public Button WhiteEndButton;
        public Button BlackEndButton;

        public IPlayerView WhitePlayerView => WhitePlayer;
        public IPlayerView BlackPlayerView => BlackPlayer;
        public new IBoardView BoardView => Board;

        public override void SetAgent(IPlayerView view, IArbiterAgent agent)
        {
            base.SetAgent(view, agent);

            WhitePlayerView.SetAgent(WhitePlayerView, Agent.WhitePlayerAgent);
            BlackPlayerView.SetAgent(BlackPlayerView, Agent.BlackPlayerAgent);

            var model = Agent.Model;
            model.GameState.DistinctUntilChanged().Subscribe(c => StateText.text = $"{c}").AddTo(this);
            model.CurrentPlayer.DistinctUntilChanged().Subscribe(c => CurrentPlayerText.text = $"{c}").AddTo(this);

            SetupUi();
        }

        public void SetupUi()
        {
            Agent.CurrentPlayerAgent.Subscribe(player =>
            {
                WhiteEndButton.interactable = player.Color == EColor.White;
                BlackEndButton.interactable = player.Color == EColor.Black;
            });

            var whiteAgent = WhitePlayerView.Agent;
            var blackAgent = BlackPlayerView.Agent;
            var white = whiteAgent.Model;
            var black = blackAgent.Model;
            WhiteEndButton.Bind(() => whiteAgent.PushRequest(new TurnEnd(white), TurnEnded));
            BlackEndButton.Bind(() => blackAgent.PushRequest(new TurnEnd(black), TurnEnded));

            Agent.LastResponse.Subscribe(r => ResponseText.text = $"{r}");
        }

        private void TurnEnded(IResponse obj)
        {
            Assert.IsNotNull(obj);
            Assert.IsTrue(obj.Success);
            //Info($"TurnEnded for {obj.Request.Player}");
        }

        public void AddWhiteCard()
        {
            var model = WhitePlayerView.Agent.Model;
            var card = model.RandomCard();
            model.Hand.Add(card);
        }

        protected override void Step()
        {
            base.Step();
            Agent?.Step();
        }

        public EColor CurrentPlayerColor => Agent.CurrentPlayerAgent.Value.Model.Color;

        public bool CurrentPlayerOwns(IOwned owned)
        {
            Assert.IsTrue(IsValid);
            Assert.IsNotNull(owned);
            Assert.IsNotNull(owned.Owner);
            return Agent.CurrentPlayerAgent.Value.Model == owned.Owner.Value;
        }
    }
}
