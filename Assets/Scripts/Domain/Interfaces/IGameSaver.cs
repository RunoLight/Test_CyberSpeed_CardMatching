using Domain.Data;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IGameSaver
    {
        public void Save(GameStateData data);
        public GameStateData Load();
    }
}