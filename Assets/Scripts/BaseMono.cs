using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMono : MonoBehaviour
{
    private void Awake()
    {
        throw new NotImplementedException();
    }

    private void OnEnable()
    {
        Initialize();
        ListenEvents();
    }

    private void OnDisable()
    {
        StopListeningEvents();
    }

    
    protected virtual void Initialize() { }
    protected virtual void ListenEvents() { }

    protected virtual void StopListeningEvents() { }
}
