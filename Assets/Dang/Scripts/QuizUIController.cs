using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizUIController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI questionText;

    [SerializeField]
    private TextMeshProUGUI score;
    int count = 0;
    [SerializeField]
    private Button[] answerButtons;

    private GameObject currentObject;

    [SerializeField]
    private Color defaultColor = Color.white; 
    [SerializeField]
    private Color correctColor = Color.green; 
    [SerializeField]
    private Color wrongColor = Color.red;
    [SerializeField]
    private Color flashColor = Color.yellow;

    private Coroutine currentCoroutine;
    

    public void SetupUIForQuestion(QuizConfig.QuestionData question)
    {
        questionText.text = question.question;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            var button = answerButtons[i];
            if (i < question.answers.Length)
            {
                button.gameObject.SetActive(true);
                button.GetComponentInChildren<TextMeshProUGUI>().text = question.answers[i];
                ResetButtonState(button); 
            }
            else
            {
                button.gameObject.SetActive(false);
            }
        }
        if (currentObject != null)
        {
            Destroy(currentObject);
        }

        if (question.associatedObject != null)
        {
            currentObject = Instantiate(question.associatedObject);
            currentObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Associated Object is missing for question");
        }
    }

    public void HandleSubmittedAnswer(int selectedAnswerIndex, int correctAnswerIndex)
    {
        var selectedButton = answerButtons[selectedAnswerIndex];
        if (selectedAnswerIndex == correctAnswerIndex)
        {
            selectedButton.image.color = correctColor;
            count++;
            score.text = "Điểm số: " + count;
        }
        else
        {
            selectedButton.image.color = wrongColor;
        }

        var correctButton = answerButtons[correctAnswerIndex];
        //correctButton.image.color = correctColor;

        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(FlashCorrectAnswer(correctButton));

        ToggleAnswerButtons(false);
    }

    private void ToggleAnswerButtons(bool value)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].interactable = value; 
        }
    }

    private void ResetButtonState(Button button)
    {
        button.image.color = defaultColor; 
        button.interactable = true; 
    }

    private IEnumerator FlashCorrectAnswer(Button button)
    {
        float flashDuration = 1f; 
        float flashInterval = 0.2f; // Khoảng thời gian giữa các lần đổi màu

        float elapsedTime = 0f;
        bool toggle = false;

        while (elapsedTime < flashDuration)
        {
            button.image.color = toggle ? flashColor : correctColor;
            toggle = !toggle;

            elapsedTime += flashInterval;
            yield return new WaitForSeconds(flashInterval);
        }

        button.image.color = correctColor;
    }

    public void StopAllEffects()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        foreach (var button in answerButtons)
        {
            ResetButtonState(button);
        }
    }


}


