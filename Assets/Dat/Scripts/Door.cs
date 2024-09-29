using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform visual;
    [SerializeField] private Collider openBlock;
    
    private Animator _animator;
    private bool _canInteract;
    private State _state;
    
    public enum State
    {
        Open,
        Close
    }
    
    private void Awake()
    {
        _animator = visual.GetComponent<Animator>();
        _state = State.Close;
    }

    private void Open()
    {
        _animator.Play("Open");
        openBlock.isTrigger = true;
        _state = State.Close;
    }

    private void Close()
    {
        _animator.Play("Close");
        openBlock.isTrigger = false;
        _state = State.Open;
    }

    private void OnTriggerEnter(Collider other)
    {
        InteractController.Inst.ShowUp();
        _canInteract = true;
    }

    private void OnTriggerExit(Collider other)
    {
        InteractController.Inst.HideDown();
        _canInteract = false;
    }

    private void Update()
    {
        if (_canInteract)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                if (_state == State.Close) Open();
                else Close();
                InteractController.Inst.HideDown(); 
            }
        }
    }
}
