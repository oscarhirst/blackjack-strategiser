// <copyright file="Strategiser.cs" company="Oscar Hirst">
// Copyright (c) Oscar Hirst. All rights reserved.
// </copyright>

namespace BlackjackBasicStrategy
{
    public class Strategiser
    {
        /// <summary>
        /// Tells a blackjack player the action basic strategy suggests, given their hand.
        /// </summary>
        /// <param name="card1">The symbol on the first card of an initial hand.</param>
        /// <param name="card2">The symbol on the second card of an initial hand.</param>
        /// <returns>The action the player should take, given their cards (following basic strategy).</returns>
        public Action Strategise(string card1, string card2)
        {
            // int CalculateValue(string card) => 0;
            return Action.Stand;
        }
    }
}
