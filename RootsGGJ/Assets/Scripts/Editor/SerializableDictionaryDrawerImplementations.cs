using UnityEngine;
using UnityEngine.UI;
 
using UnityEditor;
using SoundDefs;

// ---------------
//  String => Int
// ---------------
[UnityEditor.CustomPropertyDrawer(typeof(StringIntDictionary))]
public class StringIntDictionaryDrawer : SerializableDictionaryDrawer<string, int> {
    protected override SerializableKeyValueTemplate<string, int> GetTemplate() {
        return GetGenericTemplate<SerializableStringIntTemplate>();
    }
}
internal class SerializableStringIntTemplate : SerializableKeyValueTemplate<string, int> {}
 
// ---------------
//  GameObject => Float
// ---------------
[UnityEditor.CustomPropertyDrawer(typeof(GameObjectFloatDictionary))]
public class GameObjectFloatDictionaryDrawer : SerializableDictionaryDrawer<GameObject, float> {
    protected override SerializableKeyValueTemplate<GameObject, float> GetTemplate() {
        return GetGenericTemplate<SerializableGameObjectFloatTemplate>();
    }
}
internal class SerializableGameObjectFloatTemplate : SerializableKeyValueTemplate<GameObject, float> {}

[UnityEditor.CustomPropertyDrawer(typeof(SoundEventSoundIdDictionary))]
public class SoundEventSoundIdDictionaryDrawer : SerializableDictionaryDrawer<SoundEvent, SoundId> {
    protected override SerializableKeyValueTemplate<SoundEvent, SoundId> GetTemplate() {
        return GetGenericTemplate<SerializableSoundEventSoundIdTemplate>();
    }
}
internal class SerializableSoundEventSoundIdTemplate : SerializableKeyValueTemplate<SoundEvent, SoundId> {}

[UnityEditor.CustomPropertyDrawer(typeof(SoundIdBooleanDictionary))]
public class SoundIdBooleanDictionaryDrawer : SerializableDictionaryDrawer<SoundId, bool> {
    protected override SerializableKeyValueTemplate<SoundId, bool> GetTemplate() {
        return GetGenericTemplate<SerializableSoundIdBooleanTemplate>();
    }
}
internal class SerializableSoundIdBooleanTemplate : SerializableKeyValueTemplate<SoundId, bool> {}

[UnityEditor.CustomPropertyDrawer(typeof(SoundIdSoundDescriptorDictionary))]
public class SoundIdSoundDescriptorDictionaryDrawer : SerializableDictionaryDrawer<SoundId, SoundDescriptor> {
    protected override SerializableKeyValueTemplate<SoundId, SoundDescriptor> GetTemplate() {
        return GetGenericTemplate<SerializableSoundIdSoundDescriptorTemplate>();
    }
}
internal class SerializableSoundIdSoundDescriptorTemplate : SerializableKeyValueTemplate<SoundId, SoundDescriptor> {}

[UnityEditor.CustomPropertyDrawer(typeof(SoundIdAudioClipDictionary))]
public class SoundIdAudioClipDictionaryDrawer : SerializableDictionaryDrawer<SoundId, AudioClip> {
    protected override SerializableKeyValueTemplate<SoundId, AudioClip> GetTemplate() {
        return GetGenericTemplate<SerializableSoundIdAudioClipTemplate>();
    }
}
internal class SerializableSoundIdAudioClipTemplate : SerializableKeyValueTemplate<SoundId, AudioClip> {}

[UnityEditor.CustomPropertyDrawer(typeof(StringSpriteDictionary))]
public class StringSpriteDictionaryDrawer : SerializableDictionaryDrawer<string, Sprite>
{
    protected override SerializableKeyValueTemplate<string, Sprite> GetTemplate()
    {
        return GetGenericTemplate<SerializableStringSpriteTemplate>();
    }
}
internal class SerializableStringSpriteTemplate : SerializableKeyValueTemplate<string, Sprite> {}