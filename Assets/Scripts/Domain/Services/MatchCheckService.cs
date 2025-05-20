using System.Collections.Generic;
using Domain.Entities;
using Domain.Interfaces;

namespace Domain.Services
{
    public class MatchCheckService : IMatchCheckService
    {
        public MatchResult TryMatch((Card first, Card second) flippedCards)
        {
            if (flippedCards.first is null || flippedCards.second is null)
            {
                return MatchResult.NoMatch();
            }

            return new MatchResult(
                flippedCards.first.PairId == flippedCards.second.PairId,
                new List<Card> { flippedCards.first, flippedCards.second }
            );
        }
    }
}