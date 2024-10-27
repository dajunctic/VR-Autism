using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Collections;
using Newtonsoft.Json;

public class ChooseLevelController : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject lessonDetailPanel;
    public GameObject chooseTypePanel;
    public GameObject chooseLevelPanel;
    public GameObject loadingPanel; 
    public Slider loadingSlider;
    public TextMeshProUGUI loadingText;
    public TextMeshProUGUI missionText;
    public Button level1;
    public Button level2;
    public Button level3;
    public Button closeButton;
    public Button backButton;

    void Start()
    {
        chooseTypePanel.SetActive(false);
        level1.onClick.AddListener(() => SetEnvironmentLevel(1));
        level2.onClick.AddListener(() => SetEnvironmentLevel(2));
        level3.onClick.AddListener(() => SetEnvironmentLevel(3));
        closeButton.onClick.AddListener(HideChooseLevelPanel);
        backButton.onClick.AddListener(BackChooseType);
    }

    public void ShowChooseLevelPanel(string mission)
    {
        missionText.text = mission;
        chooseLevelPanel.SetActive(true);
    }

    private void SetEnvironmentLevel(int level)
    {
        GameSession.Instance.Level = level;
        Debug.Log("Level Set to: " + level);
    }

    void HideChooseLevelPanel()
    {
        chooseLevelPanel.SetActive(false);
    }

    void BackChooseType()
    {
        chooseLevelPanel.SetActive(false);
        chooseTypePanel.SetActive(true);
    }

    public void StartGame()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        var gameData = JsonConvert.SerializeObject(GameSession.Instance);
        PlayerPrefs.SetString("GameSession", gameData);
        string sceneName = DetermineSceneName();
        //AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("ConvaiDemo");
        Debug.Log("Loading scene: " + sceneName);

        // Hide the current panel and show the loading panel
        chooseLevelPanel.SetActive(false);
        menuPanel.SetActive(false);
        lessonDetailPanel.SetActive(false);
        chooseTypePanel.SetActive(false);
        loadingPanel.SetActive(true);
        //loadBackground.sprite = GameSession.Instance.BackgroundCover;

        // While the asynchronous operation to load the new scene is not yet complete, continue updating the slider
        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f); 
            loadingSlider.value = progress;
            loadingText.text = "Loading... " + (progress * 100).ToString("F0") + "%";
            yield return null;
        }
    }

    private string DetermineSceneName()
    {
        // Example: Lesson1_Distractor_Level1
        return "Lesson" + GameSession.Instance.LessonID;
        // return "Lesson" + GameSession.Instance.LessonID + "_" + GameSession.Instance.EnvironmentType + "_Level" + GameSession.Instance.Level;
    }
}
