﻿using Dekuple;
using Dekuple.Agent;
using Dekuple.View;
using Dekuple.View.Impl;
using UnityEngine.UI;

using UniRx;
using UnityEngine;

namespace App.View.Impl1
{
    using Agent;
    using Common;
    using Common.Message;

    /// <summary>
    /// View of an Arbiter. This is mostly related to meta-data about the game state.
    /// </summary>
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
        public EColor CurrentPlayerColor => Agent.CurrentPlayerAgent.Value.Model.Color;
        public AudioClip[] MusicClips;
        public AudioClip[] EndTurnClips;
        
        public IPlayerView WhitePlayerView => WhitePlayer;
        public IPlayerView BlackPlayerView => BlackPlayer;
        public IPlayerView CurrentPlayerView => CurrentPlayerColor == EColor.White ? WhitePlayerView : BlackPlayerView;
        public IBoardView BoardView => Board;

        private GameRoot _gameRoot;

        public void SetAgent(IPlayerView view, IArbiterAgent agent)
        //public override void SetAgent(IViewBase view, IAgent agent)
        {
            base.SetAgent(view, agent);

            PlayMusic();

            WhitePlayerView.SetAgent(WhitePlayerView, Agent.WhitePlayerAgent);
            BlackPlayerView.SetAgent(BlackPlayerView, Agent.BlackPlayerAgent);

            var model = Agent.Model;
            model.GameState.DistinctUntilChanged().Subscribe(c => StateText.text = $"{c}").AddTo(this);
            model.CurrentPlayer.DistinctUntilChanged().Subscribe(c => CurrentPlayerText.text = $"{c.Color}").AddTo(this);

            _gameRoot = transform.parent.GetComponent<GameRoot>();

            SetupUi();
        }

        public bool CurrentPlayerOwns(IOwned owned)
        {
            Assert.IsTrue(IsValid);
            Assert.IsNotNull(owned);
            Assert.IsNotNull(owned.Owner);
            return Agent.CurrentPlayerAgent.Value.Model == owned.Owner.Value;
        }

        private void PlayMusic()
        {
            _AudioSource.clip = MusicClips[0];
            _AudioSource.loop = true;
            _AudioSource.volume = 0.5f;
            _AudioSource.Play();
        }

        private void SetupUi()
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
            
            Agent.LastResponse.Subscribe(
                (r) =>
                {
                    ResponseText.text = $"{r}";
                    #if DEBUG
                    _gameRoot.CheckAllValid();
                    #endif
                }
            );
        }

        private void TurnEnded(IResponse obj)
        {
            _AudioSource.PlayOneShot(EndTurnClips[0]);
            Assert.IsNotNull(obj);
            Assert.IsTrue(obj.Success);
            Verbose(5, $"TurnEnded for {obj.Request.Owner}");
        }

        protected override void Step()
        {
            base.Step();
            Agent?.Step();
        }
    }
}
