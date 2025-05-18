using System.Collections.Generic;
using Domain.Data;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure
{
    public class SimpleFallbackCardProvider : ILoadFallbackCardsProvider
    {
        private readonly int _rows;
        private readonly int _cols;

        public SimpleFallbackCardProvider(int rows, int cols)
        {
            _rows = rows;
            _cols = cols;
        }

        public List<Card> CreateInitialCards()
        {
            return CardGenerator.CreateCards(_rows, _cols);
        }
    }
}