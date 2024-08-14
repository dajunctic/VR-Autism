using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float interactRange = 2f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            var colliderArray = Physics.OverlapSphere(transform.position, interactRange);

            foreach (var collider in colliderArray)
            {
                if (collider.TryGetComponent<NPCInteractable>(out var npcInteractable))
                {
                    npcInteractable.Interact();
                }
            }
        }
    }
}
