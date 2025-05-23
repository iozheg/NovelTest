using System.Collections.Generic;

using UnityEngine;

public class GenericAudioManager<T, SoundType> : MonoBehaviour, IDataPersistence<T>
    where T : IDataStorage, IAudioData, new()
    where SoundType : System.Enum
{
    public static GenericAudioManager<T, SoundType> Instance { get; private set; }

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private AudioSource _bgAudioSource;
    protected Dictionary<SoundType, AudioClip> _soundDictionary = new ();
    protected AudioClip _mainScreenMusic;
    protected AudioClip _gameScreenMusic;

    private DataPersistenceManager<T> _dataManager;

    public bool IsMusicOn { get; private set; } = true;
    public bool IsSoundOn { get; private set; } = true;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Initialize(DataPersistenceManager<T> dataHandler)
    {
        _dataManager = dataHandler;
        _dataManager.RegisterDataSource(this);
    }

    public virtual void Setup(AudioOptions<SoundType> audioOptions)
    {
        _audioSource.volume = audioOptions.SoundVolume;
        _bgAudioSource.volume = audioOptions.MusicVolume;

        _bgAudioSource.loop = true;

        _mainScreenMusic = audioOptions.MainScreenMusic;
        _gameScreenMusic = audioOptions.GameScreenMusic;
        
        _bgAudioSource.clip = _mainScreenMusic;

        _soundDictionary = audioOptions.SoundClips;
    }

    public void SetBackgroundMusic(AudioClip music)
    {
        _bgAudioSource.clip = music;
        if (IsMusicOn)
        {
            _bgAudioSource.Play();
        }
    }

    public void Play(SoundType soundType, float volumeScale = 1f)
    {
        if (_soundDictionary.TryGetValue(soundType, out var clip))
        {
            _audioSource.PlayOneShot(clip, volumeScale);
        }
    }

    public void Play(AudioClip clip, float volumeScale = 1f)
    {
        _audioSource.PlayOneShot(clip, volumeScale);
    }

    public void ToggleMusic()
    {
        bool musicToggle = !IsMusicOn;
        SetMusicState(musicToggle);

        _dataManager.SaveGame();
    }

    public void SoundToggle()
    {
        bool soundToggle = !IsSoundOn;
        SetSoundState(soundToggle);

        _dataManager.SaveGame();
    }

    public void MuteToggle(bool isMute)
    {
        _bgAudioSource.mute = isMute || !IsMusicOn;
        _audioSource.mute = isMute || !IsSoundOn;
        if (isMute)
        {
            _audioSource.Pause();
        }
        else
        {
            _audioSource.UnPause();
        }
    }

    public void SetDataManager(DataPersistenceManager<T> dataManager)
    {
    }

    public void LoadData(T data)
    {
        data.GetAudioState(out bool isMusicOn, out bool isSoundOn);
        SetMusicState(isMusicOn);
        SetSoundState(isSoundOn);
    }

    public void SaveData(T data)
    {
        data.SetAudioState(IsMusicOn, IsSoundOn);
    }

    private void SetMusicState(bool isMusicOn)
    {
        if (isMusicOn)
        {
            _bgAudioSource.Play();
        }
        else
        {
            _bgAudioSource.Pause();
        }

        IsMusicOn = isMusicOn;
    }

    private void SetSoundState(bool isSoundOn)
    {
        _audioSource.mute = !isSoundOn;

        IsSoundOn = isSoundOn;
    }
}

public struct AudioOptions<SoundType>
{
    public float SoundVolume;
    public float MusicVolume;
    public AudioClip MainScreenMusic;
    public AudioClip GameScreenMusic;
    public Dictionary<SoundType, AudioClip> SoundClips;
}