using System;
using System.Collections;
using System.Collections.Generic;
using Daark;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<SoundObject> soundObjects;

    private void Awake()
    {
        this.SubscribeListener(EventID.PlaySound, param => PlaySound((TypeSound) param));
    }

    private void PlayMusic()
    {
        
    }

    private void PlaySound(TypeSound typeSound)
    {
        var sound = soundObjects.Find(x => x.typeSound == typeSound);
        if (sound is null) return;
        audioSource.PlayOneShot(sound.audioClip);
    }
}

[Serializable]
public class SoundObject
{
    public TypeSound typeSound;
    public AudioClip audioClip;
}

public enum TypeSound
{
    None,

    // Grass Land Animal Lesson
    GrassLandSound,
    GrassLandIntro,
    GrassLandEnd,
    GiraffeSound,
    GiraffeDes,
    RabbitSound,
    RabbitDes,
    ElephantSound,
    ElephantDes,
    ZebraSound,
    ZebraDes,
    LionSound,
    LionDes,

    
    // Farm Animal Lesson
    FarmSound,
    FarmIntro,
    FarmEnd,
    ChickenSound,
    ChickenDesc,
    DogSound,
    DogDes,
    SheepSound,
    SheepDes,
    CowSound,
    CowDes,
    PigSound,
    PigDes,

    // Ocean Animal Lesson
    OceanSound,
    OceanIntro,
    OceanEnd,
    SharkSound,
    SharkDes,
    DolphinSound,
    DolphinDes,
    JellyfishSound,
    JellyfishDes,
    TurtleSound,
    TurtleDes,
   
}


