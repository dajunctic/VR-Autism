using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Serialization;

public class LessonDetailUI : MonoBehaviour
{
    public GameObject lessonDetailPanel;
    public GameObject chooseTypePanel;
    public ChooseTypeUI chooseTypeUI;
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
        if (cover!= null) coverImage.sprite = cover;
        lessonDetailPanel.SetActive(true);
    }

   
    void HideLessonDetailPanel()
    {
        lessonDetailPanel.SetActive(false);
    }

    private void ProceedToNextStep()
    {
        chooseTypeUI.ShowChooseTypePanel(titleText.text);
        lessonDetailPanel.SetActive(false);
        chooseTypePanel.SetActive(true);
    }

}
