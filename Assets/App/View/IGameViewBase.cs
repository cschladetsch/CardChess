﻿using App.Model;
using Dekuple.View;

namespace App.View
{
    public interface IGameViewBase
        : IViewBase
    {
        IPlayerView PlayerView { get; set; }
        IPlayerModel PlayerModel { get; }
        IArbiterView ArbiterView { get; set; }
        IBoardView BoardView { get; set; }
    }
}