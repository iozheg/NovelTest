using UnityEngine;

namespace API.Mock
{
    public class PlayerAPI<GameDataStorage> : Interfaces.IPlayerAPI<GameDataStorage> 
        where GameDataStorage : new()
    {
        private readonly IDataHandler<GameDataStorage> _dataHandler;

        public PlayerAPI()
        {
            _dataHandler = new FileDataHandler<GameDataStorage>(
                Application.persistentDataPath,
                "gameData.json"
            );
        }

        public string GetPlayerId()
        {
            return "MockPlayerId";
        }

        public GameDataStorage Load()
        {
            return _dataHandler.Load();
        }

        public void Save(GameDataStorage gameData)
        {
            _dataHandler.Save(gameData);
        }

    }
}