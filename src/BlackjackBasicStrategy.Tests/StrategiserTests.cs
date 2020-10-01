// <copyright file="StrategiserTests.cs" company="Oscar Hirst">
// Copyright (c) Oscar Hirst. All rights reserved.
// </copyright>

namespace BlackjackBasicStrategy.Tests
{
    using NUnit.Framework;

    public class StrategiserTests
    {
        private static readonly Strategiser Strategiser = new Strategiser();

        [SetUp]
        public void Setup()
        {
        }

        [TestCase("K", "7")]
        [TestCase("Q", "7")]
        [TestCase("J", "7")]
        [TestCase("10", "7")]
        [TestCase("9", "8")]
        public void Strategise_GivenHard17_Stands(string card1, string card2)
        {
            Assert.That(Strategiser.Strategise(card1, card2), Is.EqualTo(Action.Stand));
        }
    }
}
