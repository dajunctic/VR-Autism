using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    public GameObject lessonDetailPanel;
    public LessonDetailController lessonDetailController;
    public List<Lesson> lessons = new List<Lesson>();

    private void Awake()
    {
        instance = this;
        lessonDetailPanel.SetActive(false);
    }

    public void ShowLessonDetail(int lessonID)
    {
        Lesson lesson = lessons.Find(l => l.ID == lessonID);
        if (lesson != null)
        {
            GameSession.Instance.LessonID = lessonID;
            lessonDetailController.ShowLessonDetailPanel(lesson.Title, lesson.Description, lesson.Cover);
        }
        else
        {
            Debug.LogWarning("Lesson not found with ID: " + lessonID);
        }
    }
    // Start is called before the first frame update
    public void LoadConvaiDemo()
    {
        SceneManager.LoadScene("ConvaiDemo");
    }

    public void LoadDemo1()
    {
        SceneManager.LoadScene("Demo 1");
        Debug.Log("Clicked: ");
    }

    public void LoadDemo2()
    {
        SceneManager.LoadScene("Demo 2");
    }

    public void LoadDemo3()
    {
        SceneManager.LoadScene("Demo 3");
    }
}
