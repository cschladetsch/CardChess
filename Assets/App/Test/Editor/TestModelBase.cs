﻿using System.Linq;
using App.Model.Impl;
using Dekuple.Model;
using Dekuple.Registry;
using NUnit.Framework;

namespace App.Model.Test
{
    using Mock;
    using Mock.Model;

    [TestFixture]
    class TestBaseModel : Flow.Impl.Logger
    {
        protected IRegistry<IModel> _reg;
        protected IBoardModel _board;
        protected IPlayerModel _white;
        protected IPlayerModel _black;
        protected IArbiterModel _arbiter;

        [SetUp]
        public void Setup()
        {
            SetupTest();
        }

        [TearDown]
        public void TearDown()
        {
            TearDownTest();
        }

        protected virtual void SetupTest()
        {
            _reg = new Registry<IModel>();
            _reg.Bind<Service.ICardTemplateService, Service.Impl.CardTemplateService>(new Service.Impl.CardTemplateService());
            _reg.Bind<IBoardModel, BoardModel>(new BoardModel(8, 8));
            _reg.Bind<IArbiterModel, ArbiterModel>(new ArbiterModel());
            _reg.Bind<IWhitePlayerModel, WhitePlayerModel>();
            _reg.Bind<IBlackPlayerModel, BlackPlayerModel>();
            _reg.Bind<ICardModel, CardModel>();
            _reg.Bind<IDeckModel, MockDeck>();
            _reg.Bind<IHandModel, MockHand>();
            _reg.Bind<IPieceModel, PieceModel>();
            _reg.Bind<IEndTurnButtonModel, EndTurnButtonModel>();
            _reg.Resolve();

            _board = _reg.Get<IBoardModel>();
            _arbiter = _reg.Get<IArbiterModel>();
            _white = _reg.Get<IWhitePlayerModel>();
            _black = _reg.Get<IBlackPlayerModel>();

            foreach (var model in _reg.Instances.ToList())
                model.PrepareModels();
        }

        protected virtual void TearDownTest()
        {
            _arbiter.Destroy();
            _white.Destroy();
            _black.Destroy();
            _board.Destroy();
        }

        protected virtual void PrepareBindings()
        {
        }
    }
}
