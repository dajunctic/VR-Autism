using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LessonDetailController : MonoBehaviour
{
    public GameObject lessonDetailPanel; 
    public Text titleText; 
    public Text descriptionText; 
    public Button closeButton;
    public Button playButton;
    public string sceneName;


    void Start()
    {
        closeButton.onClick.AddListener(HideLessonDetailPanel);
        playButton.onClick.AddListener(() => LoadLessonScene(sceneName));
        lessonDetailPanel.SetActive(false); 
    }


    public void ShowLessonDetailPanel(string title, string description)
    {
        titleText.text = title;
        descriptionText.text = description;
        lessonDetailPanel.SetActive(true);
    }

   
    void HideLessonDetailPanel()
    {
        lessonDetailPanel.SetActive(false);
    }

    public void LoadLessonScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
