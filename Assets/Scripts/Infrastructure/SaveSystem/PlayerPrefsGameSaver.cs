using Domain.Entities;
using Domain.Interfaces;
using UnityEngine;

namespace Infrastructure.SaveSystem
{
    public class PlayerPrefsGameSaver : IGameSaver
    {
        private const string Path = "save";
        
        public void Save(GameStateData data)
        {
            var json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(Path, json);
        }

        public GameStateData Load()
        {
            if (!PlayerPrefs.HasKey(Path))
                return null;
                
            var json = PlayerPrefs.GetString(Path);
            return JsonUtility.FromJson<GameStateData>(json);
        }
    }
}