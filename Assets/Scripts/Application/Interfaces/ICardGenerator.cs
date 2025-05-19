using System.Collections.Generic;
using Domain.Data;

namespace Application.Interfaces
{
    public interface ICardGenerator
    {
        public List<CardData> CreateCards(int rows, int cols);
    }
}