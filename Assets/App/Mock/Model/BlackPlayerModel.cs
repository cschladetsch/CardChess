﻿using System;
using System.Collections.Generic;
using App.Model;
using Dekuple;

namespace App.Mock.Model
{
    using Common;
    using Common.Message;

    public class BlackPlayerModel
        : MockModelPlayerBase
        , IBlackPlayerModel
    {
        public BlackPlayerModel()
            : base(EColor.Black)
        {
        }

        protected override void CreateActionList()
        {
            _Requests = new List<Func<IRequest>>()
            {
                //() => new StartGame(this),
                //() => new RejectCards(this),
                () => new PlacePiece(this, GetCardFromHand(EPieceType.King), new Coord(4, 5)),
                () => _EndTurn,
                () => new PlacePiece(this, GetCardFromHand(EPieceType.Peon), new Coord(3, 3)),
                () => _EndTurn,
                () => new PlacePiece(this, GetCardFromHand(EPieceType.Archer), new Coord(5,2)),
                () => _EndTurn,
                () => new PlacePiece(this, GetCardFromHand(EPieceType.Gryphon), new Coord(4,4)),
                () => _EndTurn,
                () => _EndTurn,
                () => _EndTurn,
                () => _EndTurn,
                () => _EndTurn,
                () => _EndTurn,
                () => _EndTurn,
                () => _EndTurn,
                () => _EndTurn,
                () => _EndTurn,
            };
        }
    }
}
