using Application.Interfaces;
using Domain.Data;

namespace Application.Services
{
    public class NewGameFactory
    {
        private readonly ICardGenerator _generator;
        private readonly int _rows;
        private readonly int _cols;

        public NewGameFactory(ICardGenerator generator, int rows, int cols)
        {
            _generator = generator;
            _rows = rows;
            _cols = cols;
        }

        public GameStateData Create()
        {
            return new GameStateData
            {
                cards = _generator.CreateCards(_rows, _cols)
            };
        }
    }

}