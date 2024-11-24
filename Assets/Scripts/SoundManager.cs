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
    // Animal Lesson
    WelcomeToAnimalLesson,
    NextAnimal,
    
    // Grass Land Animal Lesson
    RabbitSound,
    RabbitIntro,
    RabbitDes,
    LionSound,
    LionIntro,
    LionDes,
    ZebraSound,
    ZebraIntro,
    ZebraDes,
    BearSound,
    BearIntro,
    BearDes,
    GiraffeSound,
    GiraffeIntro,
    GiraffeDes,
    ElephantSound,
    ElephantIntro,
    ElephantDes,
    
    // Farm Animal Lesson
    ChickenSound,
    ChickenIntro,
    ChickenDesc,
    
    // lesson
    ThankParticipateLesson
}


