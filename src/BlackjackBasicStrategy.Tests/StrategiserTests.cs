// <copyright file="StrategiserTests.cs" company="Oscar Hirst">
// Copyright (c) Oscar Hirst. All rights reserved.
// </copyright>

namespace BlackjackBasicStrategy.Tests
{
    using NUnit.Framework;

    public class StrategiserTests
    {
        private static readonly Strategiser Strategiser = new Strategiser();

        /*[SetUp]
        public void Setup()
        {
        }*/

        /* Terminology:
         * Soft = hand that has an Ace as one of the first two cards, the ace counts as 11 to start
         * Hard = hand that does not start with an ace in it, or it has been dealt an ace that can only be counted as 1 instead of 11*/

        [TestCase("K", "7")]
        [TestCase("J", "7")]
        [TestCase("9", "8")]
        public void Strategise_GivenHard17_Stands(string card1, string card2)
        {
            Assert.That(Strategiser.Strategise(card1, card2, null), Is.EqualTo(Action.Stand));
        }

        [TestCase("K", "3", "6")]
        [TestCase("10", "4", "4")]
        [TestCase("7", "9", "2")]
        public void Strategise_GivenHard13to16AndDealerLessThan7_Stands(string card1, string card2, string dealerCard)
        {
            Assert.That(Strategiser.Strategise(card1, card2, dealerCard), Is.EqualTo(Action.Stand));
        }

        [TestCase("K", "2", "6")]
        [TestCase("9", "3", "5")]
        [TestCase("7", "5", "4")]
        public void Strategise_GivenHard12AndDealer4to6_Stands(string card1, string card2, string dealerCard)
        {
            Assert.That(Strategiser.Strategise(card1, card2, dealerCard), Is.EqualTo(Action.Stand));
        }

        [TestCase("A", "A", "A")]
        [TestCase("A", "A", "6")]
        [TestCase("A", "A", "2")]
        [TestCase("8", "8", "A")]
        [TestCase("8", "8", "6")]
        [TestCase("8", "8", "2")]
        public void Strategise_GivenAcesOr8s_Splits(string card1, string card2, string dealerCard)
        {
            Assert.That(Strategiser.Strategise(card1, card2, dealerCard), Is.EqualTo(Action.Split));
        }

        [TestCase("9", "9", "9")]
        [TestCase("9", "9", "5")]
        [TestCase("9", "9", "2")]
        public void Strategise_Given9sAndDealer2to9_Splits(string card1, string card2, string dealerCard)
        {
            Assert.That(Strategiser.Strategise(card1, card2, dealerCard), Is.EqualTo(Action.Split));
        }

        [Test]
        public void Strategise_Given9sAndDealer7_Stands()
        {
            Assert.That(Strategiser.Strategise("9", "9", "7"), Is.EqualTo(Action.Stand));
        }

        [TestCase("10")]
        [TestCase("Q")]
        [TestCase("A")]
        public void Strategise_Given9sAndDealerGreaterThan9_Stands(string dealerCard)
        {
            Assert.That(Strategiser.Strategise("9", "9", dealerCard), Is.EqualTo(Action.Stand));
        }

        [TestCase("2")]
        [TestCase("4")]
        [TestCase("7")]
        public void Strategise_Given7sAndDealer2To7_Splits(string dealerCard)
        {
            Assert.That(Strategiser.Strategise("7", "7", dealerCard), Is.EqualTo(Action.Split));
        }

        [TestCase("2")]
        [TestCase("4")]
        [TestCase("6")]
        public void Strategise_Given6sAndDealer2To6_Splits(string dealerCard) // Only split on 2 with DAS
        {
            Assert.That(Strategiser.Strategise("6", "6", dealerCard), Is.EqualTo(Action.Split));
        }

        [TestCase("5")]
        [TestCase("6")]
        public void Strategise_Given4sAndDealer5or6_Splits(string dealerCard)
        {
            Assert.That(Strategiser.Strategise("4", "4", dealerCard), Is.EqualTo(Action.Split));
        }

        [TestCase("2")]
        [TestCase("5")]
        [TestCase("7")]
        public void Strategise_Given3sAndDealer2to7_Splits(string dealerCard)
        {
            Assert.That(Strategiser.Strategise("3", "3", dealerCard), Is.EqualTo(Action.Split));
        }

        [TestCase("2")]
        [TestCase("5")]
        [TestCase("7")]
        public void Strategise_Given2sAndDealer2to7_Splits(string dealerCard)
        {
            Assert.That(Strategiser.Strategise("2", "2", dealerCard), Is.EqualTo(Action.Split));
        }

        [TestCase("2")]
        [TestCase("5")]
        [TestCase("7")]
        public void Strategise_Given2or3sAndDealer2to7_Splits(string dealerCard)
        {
            Assert.That(Strategiser.Strategise("2", "2", dealerCard), Is.EqualTo(Action.Split));
        }

        // Doubles
        [TestCase("2")]
        [TestCase("5")]
        [TestCase("9")]
        public void Strategise_Given5sAndDealer2To9_Doubles(string dealerCard)
        {
            Assert.That(Strategiser.Strategise("5", "5", dealerCard), Is.EqualTo(Action.Double));
        }

        [TestCase("9", "2")]
        [TestCase("8", "3")]
        [TestCase("7", "4")]
        [TestCase("5", "6")]
        public void Strategise_GivenHard11_Doubles(string card1, string card2)
        {
            Assert.That(Strategiser.Strategise(card1, card2, null), Is.EqualTo(Action.Double));
        }

        [TestCase("6", "4", "2")]
        [TestCase("3", "7", "5")]
        [TestCase("2", "8", "9")]
        public void Strategise_GivenHard10AndDealer2to9_Doubles(string card1, string card2, string dealerCard)
        {
            Assert.That(Strategiser.Strategise(card1, card2, dealerCard), Is.EqualTo(Action.Double));
        }

        // 9 doubles against dealer 3 through 6 otherwise hit.

        // Hit Guard Tests
        [TestCase("K", "3", "7")]
        [TestCase("10", "4", "10")]
        [TestCase("7", "9", "K")]
        public void Strategise_GivenHard13to16AndDealerGreaterThan6_Hits(string card1, string card2, string dealerCard)
        {
            Assert.That(Strategiser.Strategise(card1, card2, dealerCard), Is.EqualTo(Action.Hit));
        }

        [TestCase("8")]
        [TestCase("J")]
        [TestCase("A")]
        public void Strategise_Given7sAndDealerGreaterThan7_Hits(string dealerCard)
        {
            Assert.That(Strategiser.Strategise("7", "7", dealerCard), Is.EqualTo(Action.Hit));
        }

        [TestCase("7")]
        [TestCase("10")]
        [TestCase("A")]
        public void Strategise_Given6sAndDealerGreaterThan6_Hits(string dealerCard)
        {
            Assert.That(Strategiser.Strategise("6", "6", dealerCard), Is.EqualTo(Action.Hit));
        }

        [TestCase("10")]
        [TestCase("Q")]
        [TestCase("A")]
        public void Strategise_Given5sAndDealerGreaterThan9_Hits(string dealerCard)
        {
            Assert.That(Strategiser.Strategise("5", "5", dealerCard), Is.EqualTo(Action.Hit));
        }

        [TestCase("2")]
        [TestCase("4")]
        [TestCase("7")]
        [TestCase("A")]
        public void Strategise_Given4sAndDealerNot5or6_Hits(string dealerCard)
        {
            Assert.That(Strategiser.Strategise("4", "4", dealerCard), Is.EqualTo(Action.Hit));
        }

        [TestCase("8")]
        [TestCase("10")]
        [TestCase("A")]
        public void Strategise_Given3sAndDealerGreaterThan7_Hits(string dealerCard)
        {
            Assert.That(Strategiser.Strategise("3", "3", dealerCard), Is.EqualTo(Action.Hit));
        }
    }
}
