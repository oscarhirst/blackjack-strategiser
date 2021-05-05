// <copyright file="Strategiser.cs" company="Oscar Hirst">
// Copyright (c) Oscar Hirst. All rights reserved.
// </copyright>

namespace BlackjackBasicStrategy
{
    using System.Runtime.Serialization.Formatters;
    using System.Security.Cryptography.X509Certificates;

    public class Strategiser
    {
        // todo: merge arrays into 3d array?
        private static readonly Action?[,] CardPairAction = new Action?[9, 9]
        {
            // 2
            { Action.Split, Action.Split, Action.Split, Action.Split, Action.Split, Action.Split, null, null, null, }, // 2
            { Action.Split, Action.Split, Action.Split, Action.Split, Action.Split, Action.Split, Action.Hit, Action.Hit, Action.Hit, }, // 3
            { null, null, null, Action.Split, Action.Split, null, null, null, null, }, // 4
            { Action.Double, Action.Double, Action.Double, Action.Double, Action.Double, Action.Double, Action.Double, Action.Double, null, }, // 5
            { Action.Split, Action.Split, Action.Split, Action.Split, Action.Split, null, null, null, null, }, // 6
            { Action.Split, Action.Split, Action.Split, Action.Split, Action.Split, Action.Split, null, null, null, }, // 7
            { Action.Split,  Action.Split,  Action.Split,  Action.Split,  Action.Split,  Action.Split,  Action.Split,  Action.Split,  Action.Split,  }, // 8
            { Action.Split,  Action.Split,  Action.Split,  Action.Split,  Action.Split,  Action.Stand,  null,  Action.Split,  Action.Stand,  }, // 9
            { null, null, null, null, null, null, null, null, null, }, // 10
        };

        private static readonly Action?[,] HardHandTotalActions = new Action?[,]
        {
            // 2
            { null, null, null, null, null, null, null, null, null, }, // 8
            { Action.Hit,  Action.Double,  Action.Double,  Action.Double,  Action.Double, Action.Hit, Action.Hit, Action.Hit, Action.Hit, }, // 9
            { Action.Double, Action.Double, Action.Double, Action.Double, Action.Double, Action.Double, Action.Double, Action.Double, null, }, // 10
            { Action.Double, Action.Double, Action.Double, Action.Double, Action.Double, Action.Double, Action.Double, Action.Double, Action.Double, }, // 11
            { null, null, Action.Stand, Action.Stand, Action.Stand, null, null, null, null, }, // 12
            { Action.Stand, Action.Stand, Action.Stand, Action.Stand, Action.Stand, Action.Hit, Action.Hit, Action.Hit, Action.Hit, }, // 13
            { Action.Stand, Action.Stand, Action.Stand, Action.Stand, Action.Stand, Action.Hit, Action.Hit, Action.Hit, Action.Hit, }, // 14
            { Action.Stand, Action.Stand, Action.Stand, Action.Stand, Action.Stand, Action.Hit, Action.Hit, Action.Hit, Action.Hit, }, // 15
            { Action.Stand, Action.Stand, Action.Stand, Action.Stand, Action.Stand, Action.Hit, Action.Hit, Action.Hit, Action.Hit, }, // 16
            { Action.Stand, Action.Stand, Action.Stand, Action.Stand, Action.Stand, Action.Stand, Action.Stand, Action.Stand, Action.Stand, }, // 17
        };

        /// <summary>
        /// Tells a blackjack player the action that basic strategy suggests, given their hand.
        /// </summary>
        /// <param name="card1">The symbol on the first card of an initial hand.</param>
        /// <param name="card2">The symbol on the second card of an initial hand.</param>
        /// <param name="dealerCard">The symbol on the dealer's up card.</param>
        /// <returns>The action the player should take, given their cards (following basic strategy).</returns>
        public Action Strategise(string card1, string card2, string dealerCard)
        {
            const string ace = "A";

            int CalculateCardValue(string card)
            {
                if (int.TryParse(card, out var value))
                {
                    return value;
                }

                return 10; // A will be handled later, return int[] ?
            }

            /*
             * Should surrender?
             */
            if (card1 == ace && card2 == ace)
            {
                return Action.Split;
            }

            var card1Value = CalculateCardValue(card1);
            var card2Value = CalculateCardValue(card2);
            var dealerCardValue = CalculateCardValue(dealerCard);

            Action? action;

            if (card1Value == card2Value)
            {
                var cardKey = card1Value - 2;
                var dealerKey = dealerCardValue - 2;

                action = CardPairAction[cardKey, dealerKey];
                if (action != null)
                {
                    return action.Value;
                };
            }

            var handValue = card1Value + card2Value;

            action = HardHandTotalActions[handValue - 8, dealerCardValue - 2];
            if (action != null)
            {
                return action.Value;
            }

            return Action.Hit;
        }
    }
}
