﻿using System.Linq;
using UnityEngine;
using App.Common;
using App.Agent;
using App.Model;
using App.View;
using App.View.Impl1;

// field not assigned - because it is assigned in Unity3d editor
#pragma warning disable 649

namespace App
{
    /// <inheritdoc />
    /// <summary>
    /// The intended root of all non-canvas objects in the scene.
    /// </summary>
    public class GameRoot
        : View.Impl1.ViewBase
    {
        public IPlayerAgent WhitePlayerAgent;
        public IPlayerAgent BlackPlayerAgent;

        public App.Model.ModelRegistry Models;
        public App.Agent.AgentRegistry Agents;
        public App.View.ViewRegistry Views;

        public IBoardAgent BoardAgent;
        public IArbiterAgent ArbiterAgent;

        public ArbiterView ArbiterView;

        /// <summary>
        /// What makes the decisions.
        /// </summary>
        public GameObject[] Startup;

        protected override bool Create()
        {
            if (!base.Create())
                return false;

            // create *all* models required for startup here
            CreateModels();

            _arbiterModel.NewGame(_whitePlayerModel, _blackPlayerModel);

            // create *all* agents
            CreateAgents();
            Info($"Agents: {Agents.Print()}");
            foreach (var agent in Agents.Instances)
                Assert.IsNotNull(agent.BaseModel);

            CreateViews();

            ArbiterAgent.NewGame(WhitePlayerAgent, BlackPlayerAgent);
            ArbiterView.Construct(ArbiterAgent);
            ArbiterAgent.StartGame();

            return true;
        }

        protected override void Begin()
        {
            base.Begin();

        }

        void CreateModels()
        {
            Models = new ModelRegistry();
            Models.Bind<Service.ICardTemplateService, Service.Impl.CardTemplateService>();
            Models.Bind<IBoardModel, BoardModel>(new BoardModel(8, 8));
            Models.Bind<IArbiterModel, ArbiterModel>(new ArbiterModel());
            Models.Bind<ICardModel, CardModel>();
            Models.Bind<IDeckModel, DeckModel>();
            Models.Bind<IHandModel, HandModel>();
            Models.Bind<IPieceModel, PieceModel>();
            Models.Bind<IPlayerModel, PlayerModel>();
            Models.Resolve();

            _boardModel = Models.New<IBoardModel>();
            _arbiterModel = Models.New<IArbiterModel>();
            _whitePlayerModel = Models.New<IPlayerModel>(EColor.White);
            _blackPlayerModel = Models.New<IPlayerModel>(EColor.Black);

            // make all models required
            foreach (var model in Models.Instances.ToList())
                model.CreateModels();

            Info($"Models: {Models.Print()}");
        }

        private void CreateAgents()
        {
            Agents = new AgentRegistry();

            Agents.Bind<IBoardAgent, BoardAgent>(new BoardAgent(_boardModel));
            Agents.Bind<IArbiterAgent, ArbiterAgent>(new ArbiterAgent(_arbiterModel));
            Agents.Bind<ICardAgent, CardAgent>();
            Agents.Bind<IDeckAgent, DeckAgent>();
            Agents.Bind<IHandAgent, HandAgent>();
            Agents.Bind<IPieceAgent, PieceAgent>();
            Agents.Bind<IPlayerAgent, PlayerAgent>();
            Agents.Resolve();

            BoardAgent = Agents.New<IBoardAgent>();
            ArbiterAgent = Agents.New<IArbiterAgent>();
            WhitePlayerAgent = Agents.New<IPlayerAgent>(_whitePlayerModel);
            BlackPlayerAgent = Agents.New<IPlayerAgent>(_blackPlayerModel);
        }

        void CreateViews()
        {
            Views = new View.ViewRegistry();
            Views.Bind<IBoardView, App.View.Impl1.BoardView>();
            Views.Bind<ICardView, View.Impl1.CardView>();
            Views.Bind<IDeckView, View.Impl1.DeckView>();
            Views.Bind<IHandView, View.Impl1.HandView>();
            Views.Bind<IPieceView, View.Impl1.PieceView>();
            Views.Bind<IPlayerView, View.Impl1.PlayerView>();

            Views.Resolve();
        }

        protected override void Step()
        {
            base.Step();
        }

        public Model.IBoardModel _boardModel;
        public Model.IArbiterModel _arbiterModel;
        public IPlayerModel _whitePlayerModel;
        public IPlayerModel _blackPlayerModel;
    }
}
