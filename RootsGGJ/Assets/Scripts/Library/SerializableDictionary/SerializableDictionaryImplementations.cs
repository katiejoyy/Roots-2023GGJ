using System;
 
using UnityEngine;
using SoundDefs;

// ---------------
//  String => Int
// ---------------
[Serializable]
public class StringIntDictionary : SerializableDictionary<string, int> {}
 
// ---------------
//  GameObject => Float
// ---------------
[Serializable]
public class GameObjectFloatDictionary : SerializableDictionary<GameObject, float> {}

[Serializable]
public class SoundEventSoundIdDictionary : SerializableDictionary<SoundEvent, SoundId> {}

[Serializable]
public class SoundIdBooleanDictionary : SerializableDictionary<SoundId, bool> {}

[Serializable]
public class SoundIdSoundDescriptorDictionary : SerializableDictionary<SoundId, SoundDescriptor> {}

[Serializable]
public class SoundIdAudioClipDictionary : SerializableDictionary<SoundId, AudioClip> {}

[Serializable]
public class StringSpriteDictionary : SerializableDictionary<string, Sprite> {}
