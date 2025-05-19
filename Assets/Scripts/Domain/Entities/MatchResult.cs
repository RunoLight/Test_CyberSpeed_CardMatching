using System.Collections.Generic;

namespace Domain.Entities
{
    public class MatchResult
    {
        public bool IsMatch { get; }
        public IReadOnlyList<Card> Cards { get; }

        public MatchResult(bool isMatch, IReadOnlyList<Card> cards)
        {
            IsMatch = isMatch;
            Cards = cards;
        }

        public static MatchResult NoMatch() => new(false, new List<Card>());
    }
}