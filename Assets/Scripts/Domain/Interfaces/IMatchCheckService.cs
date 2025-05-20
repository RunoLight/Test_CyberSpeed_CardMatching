using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IMatchCheckService
    {
        public MatchResult TryMatch((Card first, Card second) flippedCards);
    }
}