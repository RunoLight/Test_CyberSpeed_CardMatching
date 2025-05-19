using System.Collections.Generic;
using Domain.Entities;
using Domain.Interfaces;

namespace Domain
{
    public class MatchCheckService : IMatchCheckService
    {
        private readonly IGameStateService _state;
        private readonly ISoundPlayer _sound;

        public MatchCheckService(IGameStateService state, ISoundPlayer sound)
        {
            _state = state;
            _sound = sound;
        }

        public MatchResult TryMatch(IReadOnlyList<Card> flippedCards)
        {
            if (flippedCards is not { Count: 2 })
            {
                return MatchResult.NoMatch();
            }

            var card1 = flippedCards[0];
            var card2 = flippedCards[1];

            bool isMatch = card1.PairId == card2.PairId;

            return new MatchResult(isMatch, new List<Card> { card1, card2 });
        }
    }
}