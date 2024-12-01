using System;
using Daark;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class ExitScene : BaseMono
{
    [SerializeField] private float distToCam;
    [SerializeField] private Camera cam;

    protected override void Initialize()
    {
        base.Initialize();
        gameObject.SetActive(false);
    }

    public void ShowUp()
    {
        if (cam == null) return;

        var targetPosition = cam.transform.position + cam.transform.forward * distToCam;
        transform.position = targetPosition;

        transform.LookAt(cam.transform);
        transform.Rotate(0, 180, 0); 
        
        gameObject.SetActive(true);
    }

    public void OnClickExit()
    {
        this.SendEvent(EventID.ChangeScene, SceneEnum.GameMenu);
    }

    public void OnClickRetry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
