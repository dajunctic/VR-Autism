using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.Serialization;

public class ChooseTypeUI : MonoBehaviour
{
    public GameObject chooseTypePanel;
    public GameObject chooseLevelPanel;
    public GameObject lessonDetailPanel;
    public ChooseLevelUI chooseLevelUI;
    public TextMeshProUGUI missionText;
    public Button distractorButton; 
    public Button noDistractorButton; 
    public Button nextButton;
    public Button closeButton;
    public Button backButton;

    private void Start()
    {
        chooseTypePanel.SetActive(false);
        distractorButton.onClick.AddListener(() => SetEnvironmentType("Distractor"));
        noDistractorButton.onClick.AddListener(() => SetEnvironmentType("NoDistractor"));
        nextButton.onClick.AddListener(ProceedToNextStep);
        closeButton.onClick.AddListener(HideChooseTypePanel);
        backButton.onClick.AddListener(BackLessonDetail);
    }

    public void ShowChooseTypePanel(string mission)
    {
        missionText.text = "Nhiệm vụ: " + mission;
        chooseTypePanel.SetActive(true);
    }

    private void SetEnvironmentType(string type)
    {
        GameSession.Instance.EnvironmentType = type;
        Debug.Log("Environment Type Set to: " + type);
    }

    private void ProceedToNextStep()
    {
        chooseLevelUI.ShowChooseLevelPanel(missionText.text);
        chooseTypePanel.SetActive(false);
        chooseLevelPanel.SetActive(true);
    }
    void HideChooseTypePanel()
    {
        chooseTypePanel.SetActive(false);
    }

    void BackLessonDetail()
    {
        chooseTypePanel.SetActive(false);
        lessonDetailPanel.SetActive(true);
    }    
}
