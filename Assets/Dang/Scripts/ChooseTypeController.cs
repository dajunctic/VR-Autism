using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class ChooseTypeController : MonoBehaviour
{
    public GameObject chooseTypePanel;
    public GameObject chooseLevelPanel;
    public GameObject lessonDetailPanel;
    public ChooseLevelController chooseLevelController;
    public TextMeshProUGUI missionText;
    public Button distractorButton; 
    public Button noDistractorButton; 
    public Button nextButton;
    public Button closeButton;
    public Button backButton;

    void Start()
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
        chooseLevelController.ShowChooseLevelPanel(missionText.text);
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
