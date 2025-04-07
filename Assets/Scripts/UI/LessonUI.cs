using Daark;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LessonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtTitle;
    [SerializeField] private Image imgBg;
    
    private Lesson _lesson;
    
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            this.SendEvent(EventID.ShowLessonDetail, _lesson);
        });
    }

    public void Init(Lesson lesson)
    {
        _lesson = lesson;
        if (lesson.cover != null) imgBg.sprite = lesson.cover;
        txtTitle.text = lesson.lesson_name;
    }
}
