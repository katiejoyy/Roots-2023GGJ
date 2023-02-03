using UnityEngine;
using SoundDefs;

public class OnStartSoundEvent : MonoBehaviour
{
	public void StartSound()
    {
        Debug.Log("Attempting to play sound.");
        SoundController soundController = GetComponent<SoundController>();
        soundController.OnSoundEvent(SoundEvent.OBJECT_STARTED);
    }
}
