// <copyright file="Strategiser.cs" company="Oscar Hirst">
// Copyright (c) Oscar Hirst. All rights reserved.
// </copyright>

namespace BlackjackBasicStrategy
{
    public class Strategiser
    {
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
             * Should split?
             */
            if (card1 == ace && card2 == ace)
            {
                return Action.Split;
            }

            /*bool softHand = false;
            if (card1 == ace || card2 == ace)
            {
                softHand = true;
            }*/

            var card1Value = CalculateCardValue(card1);
            var card2Value = CalculateCardValue(card2);
            var dealerCardValue = CalculateCardValue(dealerCard);

            bool IsPairOf(int cardValue) => card1Value == cardValue && card2Value == cardValue;

            if ((IsPairOf(9) && dealerCardValue != 7 && dealerCardValue < 10) ||
                IsPairOf(8) ||
                (IsPairOf(7) && dealerCardValue < 8) ||
                (IsPairOf(6) && dealerCardValue < 7) ||
                (IsPairOf(4) && (dealerCardValue == 5 || dealerCardValue == 6)) ||
                (IsPairOf(3) && dealerCardValue < 8) ||
                (IsPairOf(2) && dealerCardValue < 8))
            {
                return Action.Split;
            }

            var handValue = card1Value + card2Value;

            // Should double?
            if (handValue == 11 ||
                (dealerCardValue < 10 && (IsPairOf(5) || handValue == 10)))
            {
                return Action.Double;
            }

            // Should stand?
            if (handValue == 17 ||
                (handValue > 12 && handValue < 17 && dealerCardValue < 7) ||
                (handValue == 12 && dealerCardValue > 3 && dealerCardValue < 7) ||
                IsPairOf(9))
            {
                return Action.Stand;
            }

            return Action.Hit;
        }
    }
}
