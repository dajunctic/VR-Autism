using System;
using System.Collections;
using Dajunctic.Scripts.Events;
using UnityEngine;
using UnityEngine.Events;

public class SpeechResponser : MonoBehaviour
{
    [SerializeField] private float timeBeforePrompt = 5f; // Thời gian chờ trước khi gợi ý
    [SerializeField] private BooleanVariable finishCondition;
    [SerializeField] private SpeechCategory[] speechCategories; // Mảng chứa các chủ đề
    
    public Action<AudioClip> OnPrompt;
    private Coroutine silenceTimer;

    public void StartResponse()
    {
        ResetSilenceTimer();
        finishCondition.Value = false;
    }

    public void AnalyzeSpeech(string text)
    {
        text = text.ToLower();
        bool found = false;

        foreach (var category in speechCategories)
        {
            if (text.Contains(category.key))
            {
                category.hasSpoken = true;
                found = true;
            }
        }

        if (found)
        {
            if (!CheckFinish())
            {
                ResetSilenceTimer();
            }
            else
            {
                finishCondition.Value = true;
            }
        }
        
        Debug.LogError(text);
    }

    public bool CheckFinish()
    {
        foreach (var category in speechCategories)
        {
            if (!category.hasSpoken) return false;
        }
        return true;
    }

    private void ResetSilenceTimer()
    {
        if (silenceTimer != null)
        {
            StopCoroutine(silenceTimer);
        }
        silenceTimer = StartCoroutine(SilenceCountdown());
    }

    private IEnumerator SilenceCountdown()
    {
        yield return new WaitForSeconds(timeBeforePrompt);
        PromptTeacher();
    }

    private void PromptTeacher()
    {
        // string prompt = GetPrompt();
        OnPrompt?.Invoke(GetPrompt());
        // Debug.Log("Gợi ý giáo viên: " + prompt);
    }

    private AudioClip GetPrompt()
    {
        foreach (var category in speechCategories)
        {
            if (!category.hasSpoken)
                return category.GetRandomAudio();
        }
        
        return null;
        // return "Con có thể kể cho cô nghe một điều thú vị về mình không?";
    }
}

[Serializable]
public class SpeechCategory
{
    public string key;               // Từ khóa cần nhận diện
    public string[] prompts; // Mảng câu hỏi gợi ý
    public AudioClip[] audios;
    public bool hasSpoken = false;   // Kiểm tra xem trẻ đã nói về chủ đề này chưa

    // Lấy một câu gợi ý ngẫu nhiên từ mảng
    public string GetRandomPrompt()
    {
        if (prompts.Length > 0)
        {
            return prompts[UnityEngine.Random.Range(0, prompts.Length)];
        }
        return "Hãy chia sẻ thêm về con nào!";
    }

    public AudioClip GetRandomAudio()
    {
        if (audios.Length > 0)
        {
            return audios[UnityEngine.Random.Range(0, audios.Length)];
        }
        return null;
    }
}