using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private bool isRainPlayingAtBegining;

    [SerializeField]
    private bool isMusicPlayingAtBegining;

    private uint? rainEventId;
    private uint? musicEventId;

    private void Start()
    {
        if (isRainPlayingAtBegining) StartRainEvent();
        if (isMusicPlayingAtBegining) StartMusicEvent();
    }

    public void PlaySound(string wwiseEventName)
    {
        AkSoundEngine.PostEvent(wwiseEventName, gameObject);
    }

    public void StartRainEvent()
    {
        if (rainEventId == null)
            rainEventId = AkSoundEngine.PostEvent("Game_Loaded", gameObject);
    }

    public void StopRainEvent()
    {
        if (rainEventId != null)
        {
            AkSoundEngine.StopPlayingID((uint)rainEventId, 0);
            rainEventId = null;
        }
    }

    public void StartMusicEvent()
    {
        if (musicEventId == null)
            musicEventId = AkSoundEngine.PostEvent("Map_Loaded", gameObject);
    }

    public void StopMusicEvent()
    {
        if (musicEventId != null)
        {
            AkSoundEngine.StopPlayingID((uint)musicEventId);
            musicEventId = null;
        }
    }
}
