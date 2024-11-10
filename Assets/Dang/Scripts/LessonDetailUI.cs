using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Serialization;

public class LessonDetailUI : MonoBehaviour
{
    public ChooseTypeUI chooseTypeUI;
    public TextMeshProUGUI titleText; 
    public TextMeshProUGUI descriptionText;
    public Image coverImage;
    public Button closeButton;
    public Button playButton;


    private void Awake()
    {
        closeButton.onClick.AddListener(Hide);
        playButton.onClick.AddListener(ProceedToNextStep);
    }


    public void Show(string title, string description, Sprite cover)
    {
        titleText.text = title;
        descriptionText.text = description;
        if (cover!= null) coverImage.sprite = cover;
        gameObject.SetActive(true);
    }

   
    void Hide()
    {
        gameObject.SetActive(false);
    }
    
    private void ProceedToNextStep()
    {
        chooseTypeUI.ShowChooseTypePanel(titleText.text);
        gameObject.SetActive(false);
    }

}
