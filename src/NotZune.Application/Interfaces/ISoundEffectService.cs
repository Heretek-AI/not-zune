namespace NotZune.Application.Interfaces;

public interface ISoundEffectService
{
    bool SoundEffectsEnabled { get; set; }
    void PlaySyncComplete();
    void PlayDownloadComplete();
    void PlayRipComplete();
    void PlayNotification();
}
