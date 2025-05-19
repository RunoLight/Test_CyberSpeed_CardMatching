using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IMatchCheckService
    {
        public MatchResult TryMatch(IReadOnlyList<Card> flippedCards);
    }
}