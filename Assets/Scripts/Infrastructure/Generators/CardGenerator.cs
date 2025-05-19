using System.Collections.Generic;
using System.Linq;
using Application.Interfaces;
using Domain.Data;
using Domain.Entities;

namespace Infrastructure.Generators
{
    public class CardGenerator : ICardGenerator
    {
        public List<CardData> CreateCards(int rows, int cols)
        {
            int pairCount = rows * cols / 2;
            var cards = new List<CardData>();
            int id = 0;

            for (int pairId = 0; pairId < pairCount; pairId++)
            {
                cards.Add(new CardData(new Card(id++, pairId)));
                cards.Add(new CardData(new Card(id++, pairId)));
            }

            var rnd = new System.Random();
            return cards.OrderBy(_ => rnd.Next()).ToList();
        }
    }
}