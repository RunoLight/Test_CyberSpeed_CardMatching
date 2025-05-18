using System.IO;
using Domain.Data;
using Domain.Entities;
using Domain.Interfaces;
using UnityEngine;

namespace Infrastructure.SaveSystem
{
    public class JsonGameSaver : IGameSaver
    {
        private static string Path => UnityEngine.Application.persistentDataPath + "/save.json";

        public void Save(GameStateData data)
        {
            var json = JsonUtility.ToJson(data);
            File.WriteAllText(Path, json);
        }

        public GameStateData Load()
        {
            if (!File.Exists(Path))
                return null;

            var json = File.ReadAllText(Path);
            return JsonUtility.FromJson<GameStateData>(json);
        }
    }
}