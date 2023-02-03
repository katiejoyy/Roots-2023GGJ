using UnityEngine;
using SoundDefs;
using System;
using System.Collections.Generic;

/* Sound Controller should be placed on anything
with an AudioSource to play sound through the sound manager.

When it starts up it caches the sound manager
Has a map of events to soundids
When it onSoundEvent is called we just call
play sound from sound manager with mapped soundid
and cached audio source.
*/
public class SoundController : MonoBehaviour
{
	private AudioSource audioSource;
	private SoundManager soundManager;

	[SerializeField]
	private SoundEventSoundIdDictionary eventToSoundStore = SoundEventSoundIdDictionary.New<SoundEventSoundIdDictionary>();
	private Dictionary<SoundEvent, SoundId> eventToSound 
	{
	    get { return eventToSoundStore.dictionary; }
	}

	public void Awake()
	{
		audioSource = GetComponent<AudioSource>();
		if(audioSource==null)
		{
			Debug.Log("No AudioSource found from SoundController object!");
		}

		soundManager = FindObjectOfType<SoundManager>();
		if(soundManager==null)
		{
			Debug.Log("Could not find audio manager!");
		}
	}

	public void OnSoundEvent(SoundEvent eventid)
	{
		if(!eventToSound.ContainsKey(eventid))
		{
			Debug.Log("Received sound event with id " + eventid + " but no associated soundid");
			return;
		}

		Debug.Log("Attempting to play audio for event with id: " + eventid);
		soundManager.PlayAudio(eventToSound[eventid], audioSource);
	}
}
