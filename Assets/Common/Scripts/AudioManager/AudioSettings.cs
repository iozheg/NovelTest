using System;
using System.Collections.Generic;

using GameConfigs;

using UnityEngine;

[CreateAssetMenu(menuName = "Game Settings/Audio Settings")]
public class AudioSettings : ScriptableObject
{
    public float SoundVolume = 1;
    public float MusicVolume = 1;
    public AudioClip BuildingScreenMusic;
    public AudioClip GameScreenMusic;
    public List<SoundStruct> Sounds;
}

[Serializable]
public class SoundStruct {
    public SoundType Type;
    public AudioClip Clip;
}