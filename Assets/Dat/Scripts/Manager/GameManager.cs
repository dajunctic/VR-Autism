using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Inst;
    private void Awake()
    {
        Inst = this;
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        
    }
}
