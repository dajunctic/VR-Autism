using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dajunctic.Scripts.Quest
{
    public class QuestProgressUI : MonoBehaviour
    {
        [SerializeField] private Image bar;

        public void SetProgress(float progress)
        {
            bar.fillAmount = progress;
        }
    }
}

