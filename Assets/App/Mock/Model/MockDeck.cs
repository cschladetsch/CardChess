﻿using System;

#pragma warning disable 649

namespace App.Mock.Model
{
    using Common;
    using Registry;
    using App.Model;

    public class MockDeck
        : DeckModel
    {
        [Inject] public Service.ICardTemplateService _cardTemplateService;

        public MockDeck(ITemplateDeck templateDeck, IOwner owner)
            : base(templateDeck, owner)
        {
        }

        public override void NewGame()
        {
            foreach (var pt in _pieceTypes)
            {
                Add(_cardTemplateService.NewCardModel(Player, pt));
            }
        }

        public override void Shuffle()
        {
            // intentionally do not shuffle
        }

        public override int ShuffleIn(params ICardModel[] models)
        {
            int n = 0;
            foreach (var card in models)
            {
                AddToBottom(card);
                ++n;
            }
            return n;
        }

        private readonly EPieceType[] _pieceTypes =
        {
            EPieceType.Peon,
            EPieceType.Peon,
            EPieceType.Peon,
            EPieceType.Peon,
            EPieceType.Peon,
            EPieceType.Peon,
            EPieceType.Peon,
            EPieceType.Peon,
            EPieceType.Peon,
            EPieceType.Peon,
            EPieceType.Archer,
            EPieceType.Archer,
            EPieceType.Archer,
            EPieceType.Archer,
            EPieceType.Archer,
            EPieceType.Archer,
            EPieceType.Gryphon,
            EPieceType.Gryphon,
            EPieceType.Gryphon,
            EPieceType.Gryphon,
            EPieceType.Gryphon,
            EPieceType.Queen,
            EPieceType.Queen,
            EPieceType.Queen,
            EPieceType.Queen,
            EPieceType.Queen,
        };
    }
}