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
    
    private void Awake()
    {
        Instance = this;
        lessonDetailPanel.SetActive(false);
        this.SubscribeListener(EventID.ShowLessonDetail, param => ShowLessonDetail((Lesson)param));
        
        Init();
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
    
    public void LoadConvaiDemo()
    {
        SceneManager.LoadScene("Supermarket");
    }
}
