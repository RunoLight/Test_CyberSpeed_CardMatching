using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ILoadFallbackCardsProvider
    {
        List<Card> CreateInitialCards();
    }
}