using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App
{
    [TestClass]
    public class UnitTest1
    {
        private static Arbiter BasicSetup()
        {
            var arbiter = new Arbiter();
            var b0 = arbiter.NewModel<Model.Board, int, int>(8, 8);
            var c0 = arbiter.NewAgent<Agent.Board, Model.IBoard>(b0);

            var m0 = arbiter.NewModel<Model.Player, EColor>(EColor.White);
            var m1 = arbiter.NewModel<Model.Player, EColor>(EColor.Black);
            var p0 = arbiter.NewAgent<Agent.Player, Model.IPlayer>(m0);
            var p1 = arbiter.NewAgent<Agent.Player, Model.IPlayer>(m1);

            var d0 = arbiter.NewModel<Model.Deck, Guid, Model.IPlayer>(Guid.Empty, m0);
            var d1 = arbiter.NewModel<Model.Deck, Guid, Model.IPlayer>(Guid.Empty, m1);

            m0.SetDeck(d0);
            m1.SetDeck(d1);

            arbiter.Setup(c0, p0, p1);

            return arbiter;
        }

        [TestMethod]
        public void TestMethod1()
        {
            var arbiter = BasicSetup();

            var p0 = arbiter.WhitePlayer;
            var p1 = arbiter.BlackPlayer;
            var m0 = p0.Model;
            var m1 = p1.Model;
            var d0 = m0.Deck;
            var d1 = m1.Deck;

            Assert.IsNotNull(p0);
            Assert.IsNotNull(p0.Model);
            Assert.IsNotNull(m0.Hand);
            Assert.IsNotNull(m0.Deck);
            Assert.IsNotNull(m0.Deck.Cards);
            Assert.IsNotNull(m1.Hand);
            Assert.IsNotNull(m1.Deck);
            Assert.IsNotNull(m1.Deck.Cards);
            Assert.AreSame(p0.Model, m0);
            Assert.AreSame(p1.Model, m1);

            Assert.AreEqual(7, p0.Model.Hand.Cards.Count);
            Assert.AreEqual(7, p1.Model.Hand.Cards.Count);
            Assert.AreEqual(43, d0.Cards.Count);
            Assert.AreEqual(43, d1.Cards.Count);
        }
    }
}