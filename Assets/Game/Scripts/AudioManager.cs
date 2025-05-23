using GameConfigs;

using GameCore.Models;

using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : GenericAudioManager<GameDataStorage, SoundType>
{
    public override void Setup(AudioOptions<SoundType> audioOptions)
    {
        base.Setup(audioOptions);

        SceneManager.sceneLoaded += OnSceneLoaded;
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode _)
    {
        AudioClip bgMusic = null;
        switch (scene.name)
        {
            case "LoadingScene":
                return;
            case "BuildingScene":
                bgMusic = _mainScreenMusic;
                break;
            case "Game":
                bgMusic = _gameScreenMusic;
                break;
        }

        SetBackgroundMusic(bgMusic);
    }
}
