using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public void PlaySound(string wwiseEventName)
    {
        AkSoundEngine.PostEvent(wwiseEventName, gameObject);
    }
}
