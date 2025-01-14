using System;
using Daark;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMenuController : MonoBehaviour
{
    public static SceneMenuController Instance;
    public GameObject lessonDetailPanel;
    public LessonDetailUI lessonDetailUI;
    [SerializeField] private TopicUI[] topics;
    public LessonConfig config;
    
    public Lesson Lesson { get; set; }
    private Action<object> ShowLessonDetails;
    
    private void Awake()
    {
        Debug.LogWarning("scene controller");
        Instance = this;
        lessonDetailPanel.SetActive(false);

        ShowLessonDetails = param => ShowLessonDetail((Lesson)param);
        this.SubscribeListener(EventID.ShowLessonDetail, ShowLessonDetails);
        
        Init();
    }

    private void OnDestroy()
    {
        this.UnsubscribeListener(EventID.ShowLessonDetail, ShowLessonDetails);
    }

    private void Init()
    {
        for (var i = 0; i < topics.Length; i++)
        {
            topics[i].Init(config.topics.Find(x => x.id == i));
        }
    }

    public void ShowLessonDetail(Lesson lesson)
    {
        if (lesson != null)
        {
            Lesson = lesson;
            lessonDetailUI.Show(lesson.title, lesson.description, lesson.cover);
        }
        else
        {
            Debug.LogWarning("Lesson not found");
        }
    }
}
