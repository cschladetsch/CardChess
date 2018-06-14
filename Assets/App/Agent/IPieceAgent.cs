﻿using App.Common;
using UniRx;

namespace App.Agent
{
    using Model;

    /// <summary>
    /// Agents act on behalf of models
    /// </summary>
    public interface IPieceAgent
        : IAgent<IPieceModel>
        , ICardProperties
    {
        IReactiveProperty<Coord> Coord { get; }
    }
}
