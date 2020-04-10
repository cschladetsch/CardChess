﻿using System;
using System.Linq;
using Dekuple.Model;

namespace App.Service.Impl
{
    using Model;
    using Common;

    public class CardTemplateService
        : ModelBase
        , ICardTemplateService
    {
        public CardTemplateService()
            : base(null)
        {
        }

        public ICardTemplate GetCardTemplate(EPieceType pieceType)
        {
            var templates = Database.CardTemplates.OfType(pieceType).ToArray();
            if (templates.Length == 0)
            {
                Error($"Failed to find card template of type {pieceType}");
                return null;
            }
            if (templates.Length > 1)
            {
                Warn($"Found {templates.Length} templates of type {pieceType} - using first found");
            }

            return templates[0];
        }

        public ICardTemplate GetCardTemplate(Guid id)
        {
            return Database.CardTemplates.Get(id);
        }

        public ICardModel NewCardModel(IPlayerModel owner, ICardTemplate tmpl)
        {
            return Registry.Get<ICardModel>(tmpl, owner);
        }

        public ICardModel NewCardModel(IPlayerModel owner, EPieceType type)
        {
            var template = GetCardTemplate(type);
            if (template != null) 
                return Registry.Get<ICardModel>(owner, template);
            Error($"Failed to find card template {type} for {owner}");
            return null;
        }
    }
}
