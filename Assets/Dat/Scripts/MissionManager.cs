using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Inst;
    [SerializeField] private AudioClip demoMissionClip;
    [SerializeField] private RectTransform bgMission;
    [SerializeField] private RectTransform rawImage;
    
    [SerializeField] private TextMeshProUGUI timeText;

    public DateTime StartTime;
    public bool MissionCompleted { get; set; } = false;
    
    private void Awake()
    {
        Inst = this;
    }

    private void Start()
    {
        StartCoroutine(TalkMission());
    }

    private IEnumerator TalkMission()
    {
        yield return new WaitForSeconds(2f);
        AudioManager.Inst.Play(demoMissionClip);
        ShowUp();
    }

    private void Update()
    {
        if (!MissionCompleted)
        {
            var timeSpan = DateTime.Now - StartTime;

            timeText.text = timeSpan.ToString(@"mm\:ss");
        }
    }

    private void ShowUp()
    {
        bgMission.DOAnchorPosX(450, 0.5f).SetEase(Ease.InOutQuad);
        rawImage.DOAnchorPosX(120, 0.5f).SetEase(Ease.InOutQuad);
        StartTime = DateTime.Now;
    }
}
