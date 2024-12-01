using System;
using Daark;
using UnityEngine;

public class ExitScene : BaseMono
{
    [SerializeField] private float distToCam;
    
    private Transform _cam;
    
    public void Setup()
    {
        _cam = Camera.main.transform;
    }
    
    private void ShowUp()
    {
        if (_cam == null) return;

        var targetPosition = _cam.position + _cam.forward * distToCam;
        transform.position = targetPosition;

        transform.LookAt(_cam);
        transform.Rotate(0, 180, 0); 
        
        
        gameObject.SetActive(true);
    }

    public void Exit()
    {
        
    }

    public void Retry()
    {
        
    }
}
