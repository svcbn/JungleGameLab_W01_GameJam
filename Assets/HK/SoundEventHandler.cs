using UnityEngine;

public class SoundEventHandler : MonoBehaviour
{
    public void PlayEvent(string _eventName)
    {
        AkSoundEngine.PostEvent(_eventName, this.gameObject);
    }
}
