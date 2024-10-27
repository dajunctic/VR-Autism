using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LessonDetailController : MonoBehaviour
{
    public GameObject lessonDetailPanel;
    public GameObject chooseTypePanel;
    public ChooseTypeController chooseTypeController;
    public TextMeshProUGUI titleText; 
    public TextMeshProUGUI descriptionText;
    public Image coverImage;
    public Button closeButton;
    public Button playButton;


    void Start()
    {
        closeButton.onClick.AddListener(HideLessonDetailPanel);
        playButton.onClick.AddListener(() => ProceedToNextStep());
    }


    public void ShowLessonDetailPanel(string title, string description, Sprite cover)
    {
        titleText.text = title;
        descriptionText.text = description;
        coverImage.sprite = cover;
        lessonDetailPanel.SetActive(true);
    }

   
    void HideLessonDetailPanel()
    {
        lessonDetailPanel.SetActive(false);
    }

    private void ProceedToNextStep()
    {
        chooseTypeController.ShowChooseTypePanel(titleText.text);
        lessonDetailPanel.SetActive(false);
        chooseTypePanel.SetActive(true);
    }

}
