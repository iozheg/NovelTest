public interface IAudioData
{
    public void SetAudioState(bool isMusicOn, bool isSoundOn);
    public void GetAudioState(out bool isMusicOn, out bool isSoundOn);
}
