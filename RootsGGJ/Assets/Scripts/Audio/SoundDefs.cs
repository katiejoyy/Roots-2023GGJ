using UnityEngine;
using System;

namespace SoundDefs
{
	//if this gets too busy maybe we can just use
	//free form strings
	public enum SoundEvent
	{
		OBJECT_STARTED=0,
	};

	public enum SoundId
	{
		MAIN_GAME_TRACK=0,
		CLICK_SFX,
        SELECT_SFX,
	};

	[Serializable]
	public struct SoundDescriptor
	{
		public AudioClip soundClip;
		public bool repeat;

		public SoundDescriptor(AudioClip clip, bool shouldRepeat)
		{
			soundClip = clip;
			repeat = shouldRepeat;
		}
	};
}
