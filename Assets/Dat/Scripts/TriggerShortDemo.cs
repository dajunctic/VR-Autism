using System;
using System.Collections;
using System.Collections.Generic;
using Convai.Scripts.Runtime.Addons;
using UnityEngine;

public class TriggerShortDemo : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<ConvaiPlayerMovement>();

        if (player != null)
        {
            TutorialController.Inst.PlayShortDemo();
        }
    }
}
