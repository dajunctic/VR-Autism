using System;
using System.Collections;
using System.Collections.Generic;
using Daark;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunMonitor : BaseMono
{
    private static RunMonitor _instance;
    [SerializeField] private SceneSO sceneSO;
    [SerializeField] private ExitScene exitScene;

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

    protected override void Initialize()
    {
        OnChangeScene += param => LoadScene((SceneEnum)param);
    }
    
    
    protected override void ListenEvents()
    {
        this.SubscribeListener(EventID.ChangeScene, OnChangeScene);
    }
    
    protected override void StopListeningEvents()
    {
        this.SubscribeListener(EventID.ChangeScene, OnChangeScene);
    }
    
    private void LoadScene(SceneEnum sceneEnum)
    {
        SceneManager.LoadScene(sceneSO.GetSceneName(sceneEnum));
    }

}
