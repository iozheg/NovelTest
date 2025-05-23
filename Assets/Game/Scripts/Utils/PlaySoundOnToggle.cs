using GameConfigs;

using UnityEngine;
using UnityEngine.UI;

public class PlaySoundOnToggle : MonoBehaviour
{
    void OnEnable()
    {
        GetComponent<Toggle>().onValueChanged.AddListener(PlayButtonClickSound);
    }

    void OnDisable()
    {
        GetComponent<Toggle>().onValueChanged.RemoveListener(PlayButtonClickSound);
    }

    private void PlayButtonClickSound(bool _)
    {
        AudioManager.Instance.Play(SoundType.ButtonClicked);
    }
}
