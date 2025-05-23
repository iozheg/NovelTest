namespace API.Mock
{
    public class GameplayAPI : API.Interfaces.IGameplayAPI
    {
        private bool _gameReadyUsed = false;

        public void GameReady()
        {
            if (!_gameReadyUsed)
            {
                _gameReadyUsed = true;
                UnityEngine.Debug.Log("GameReady");
            }
        }

        public void GameplayStart()
        {
            UnityEngine.Debug.Log("GameplayStart");
        }

        public void GameplayStop()
        {
            UnityEngine.Debug.Log("GameplayStop");
        }
    }
}