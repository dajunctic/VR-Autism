using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Inst;
    [SerializeField] private AudioClip demoMissionClip;
    [SerializeField] private RectTransform bgMission;
    [SerializeField] private RectTransform rawImage;
    
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

    private void ShowUp()
    {
        bgMission.DOAnchorPosX(450, 0.5f).SetEase(Ease.InOutQuad);
        rawImage.DOAnchorPosX(120, 0.5f).SetEase(Ease.InOutQuad);
    }
}
