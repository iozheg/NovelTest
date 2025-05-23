using GameConfigs;

using UnityEngine;
using UnityEngine.UI;

public class PlaySoundOnClick : MonoBehaviour
{
    void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(PlayButtonClickSound);
    }

    void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(PlayButtonClickSound);
    }

    private void PlayButtonClickSound()
    {
        AudioManager.Instance.Play(SoundType.ButtonClicked);
    }
}
