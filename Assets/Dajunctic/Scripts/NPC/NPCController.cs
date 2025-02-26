using System.Collections;
using System.Collections.Generic;
using Convai.Scripts.Runtime.Core;
using UnityEngine;

namespace Dajunctic.Scripts.NPC
{
    public class NPCController : MonoBehaviour
    {
        [SerializeField] private ConvaiNPC[] convaiNpc;
        [SerializeField, Multiline] private string[] messages;

        private ConvaiNPC myNPC;

        public void SetNpc(int id)
        {
            myNPC = convaiNpc[id];
        }
        
        public void SaySomething(int id)
        {
            if (myNPC != null)
            {
                myNPC.TriggerSpeech("Hãy nói y hệt tôi như sau: " + messages[id]);
            }
            else
            {
                Debug.LogError("NPC chưa được gán!");
            }
        }
        
    } 
}

