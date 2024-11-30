using System;
using System.Collections;
using System.Collections.Generic;
using Daark;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunMonitor : MonoBehaviour
{
    private static RunMonitor _instance;
    [SerializeField] private SceneSO sceneSO;

    private Action<object> OnChangeScene;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this);
    }

    private void Initialize()
    {
        OnChangeScene += param => LoadScene((SceneEnum)param);
    }

    private void OnEnable()
    {
        ListenEvent();
    }

    private void OnDisable()
    {   
        StopListenEvent();
    }
    
    private void ListenEvent()
    {
        this.SubscribeListener(EventID.ChangeScene, OnChangeScene);
    }
    
    private void StopListenEvent()
    {
        this.SubscribeListener(EventID.ChangeScene, OnChangeScene);
    }
    
    private void LoadScene(SceneEnum sceneEnum)
    {
        SceneManager.LoadScene(sceneSO.GetSceneName(sceneEnum));
    }

}
