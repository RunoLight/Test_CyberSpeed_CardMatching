using System.Collections.Generic;
using System.Linq;
using Domain.Entities;

namespace Domain.Data
{
    public static class CardGenerator
    {
        public static List<Card> CreateCards(int rows, int cols)
        {
            int pairCount = (rows * cols) / 2;
            var cards = new List<Card>();
            int id = 0;

            for (int pairId = 0; pairId < pairCount; pairId++)
            {
                cards.Add(new Card(id++, pairId));
                cards.Add(new Card(id++, pairId));
            }

            var rnd = new System.Random();
            return cards.OrderBy(_ => rnd.Next()).ToList();
        }
    }

}