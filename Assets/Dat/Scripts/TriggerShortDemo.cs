using System;
using System.Collections;
using System.Collections.Generic;
using Convai.Scripts.Runtime.Addons;
using UnityEngine;

public class TriggerShortDemo : MonoBehaviour
{
    [SerializeField] private TriggerType triggerType;
    private void OnTriggerEnter(Collider other)
    {
        if (triggerType == TriggerType.Checkout)
        {
            var interactObj = other.GetComponent<IDaarkInteractable>();

            if (interactObj != null)
            {
                
            }
        }
        else
        {
            var player = other.GetComponent<ConvaiPlayerMovement>();

            if (player != null)
            {
                TutorialController.Inst.PlayShortDemo();
            }
        }
    }
}

public enum TriggerType
{
    Introduce,
    Checkout,
}
