using System.Collections;
using System.Collections.Generic;
using DialogueEditor;
using UnityEngine;

public class NPCInteractable : MonoBehaviour
{
    [SerializeField] private Conversation conversation;
    [SerializeField] private ChatBubbleUI chatBubbleUI;
    public void Interact()
    {
        Debug.Log("Start Converation with NPC");
        DialogueManager.Inst.StartConversation(conversation, chatBubbleUI);
    }
}
