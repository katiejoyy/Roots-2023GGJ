using UnityEngine;
using System;
using System.Collections.Generic;
using SoundDefs;

public class SoundManager : MonoBehaviour
{
	[SerializeField]
	private SoundIdAudioClipDictionary soundBankStore = SoundIdAudioClipDictionary.New<SoundIdAudioClipDictionary>();
	private Dictionary<SoundId, AudioClip> soundBank 
	{
	    get { return soundBankStore.dictionary; }
	}

    [SerializeField]
	private SoundIdBooleanDictionary soundPropertiesStore = SoundIdBooleanDictionary.New<SoundIdBooleanDictionary>();
	private Dictionary<SoundId, bool> soundProperties 
	{
	    get { return soundPropertiesStore.dictionary; }
	}

    /*
    //This is what we actually want to use, but for some reason, the new version of unity can't display it
    [SerializeField]
	private SoundIdSoundDescriptorDictionary soundDescriptorBankStore = SoundIdSoundDescriptorDictionary.New<SoundIdSoundDescriptorDictionary>();
	private Dictionary<SoundId, SoundDescriptor> soundDescriptorBank 
	{
	    get { return soundDescriptorBankStore.dictionary; }
	}*/

	[Range(0.0f, 1.0f)]
	public float volume;

	public void PlayAudio(SoundId soundId, AudioSource source)
	{
        if(source == null)
        {
            source = GetComponent<AudioSource>();
        }

		Debug.Log("Received request to play sound with id: " + soundId);
		if(!soundBank.ContainsKey(soundId))
		{
			Debug.Log("Could not play sound with Id: " + soundId);
			return;
		}

        bool repeat = soundProperties.ContainsKey(soundId) ? soundProperties[soundId] : false;

		source.clip = soundBank[soundId];
		if(repeat)
		{
			source.loop = true;
			source.Play();
		}
		else
		{
			source.loop = false;
			source.PlayOneShot(source.clip, volume);
		}
	}
}
