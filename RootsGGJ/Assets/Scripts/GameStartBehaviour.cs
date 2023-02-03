using SoundDefs;
using System.Collections;
using UnityEngine;

public class GameStartBehaviour : MonoBehaviour
{
    public void Start()
    {
        SoundController soundController = GetComponent<SoundController>();
        soundController.OnSoundEvent(SoundEvent.OBJECT_STARTED);
    }
}
