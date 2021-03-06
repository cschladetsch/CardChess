﻿namespace App.View
{
    using UnityEngine;
    using UniRx;
    using Dekuple;
    using Dekuple.View;
    using Common;
    using Agent;
    using Model;

    /// <summary>
    /// View of the current Board state in the scene.
    /// </summary>
    public interface IBoardView
        : IView<IBoardAgent>
        , IPrintable
    {
        IReadOnlyReactiveProperty<int> Width { get; }
        IReadOnlyReactiveProperty<int> Height { get; }
        IReadOnlyReactiveProperty<ISquareView> HoverSquare { get; }
        IReadOnlyReactiveCollection<IPieceView> Pieces { get; }

        Material BlackMaterial { get; }
        Material WhiteMaterial { get; }

        IPieceView Get(Coord coord);
        IResponse Remove(IPieceView pieceView);
        IResponse MovePiece(IPieceView pieceView, Coord coord);

        ISquareView TestRayCast(Vector3 screen);
        void ShowSquares(ICardModel cardView, ISquareView sq);
    }
}
