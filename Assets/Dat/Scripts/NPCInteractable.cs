using System.Collections;
using System.Collections.Generic;
using DialogueEditor;
using UnityEngine;

public class NPCInteractable : MonoBehaviour
{
    [SerializeField] private NPCConversation conversation;

    public void Interact()
    {
        Debug.Log("Start Converation with NPC");
        ConversationManager.Instance.StartConversation(conversation);
    }
}
