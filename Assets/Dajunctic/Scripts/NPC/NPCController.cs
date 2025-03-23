using System.Collections;
using System.Collections.Generic;
using Convai.Scripts.Runtime.Core;
using UnityEngine;

namespace Dajunctic.Scripts.NPC
{
    public class NPCController : MonoBehaviour
    {
        [SerializeField] private AudioSource[] npcs;
        [SerializeField] private AudioClip[] audioClips;

        private AudioSource myNPC;

        public void SetNpc(int id)
        {
            myNPC = npcs[id];
        }
        
        public void SaySomething(int id)
        {
            myNPC.clip = audioClips[id];
            myNPC.Play();
           // myNPC.PlayOneShot(audioClips[id]);
        }
        
    } 
}

