using System;
using System.Collections;
using System.Collections.Generic;
using Dajunctic.Scripts.Events;
using Dajunctic.Scripts.Utils;
using UnityEngine;
using UnityEngine.Serialization;

namespace Dajunctic.Scripts.Manager
{
    public class TimeManager : MonoBehaviour
    {
        [SerializeField] private LongVariable lessonTime;
        
        private LessonTimeData data;
        private void Awake()
        {
            data = new LessonTimeData();
        }

        public void StartLessonTime()
        {
            lessonTime.Value = TimeUtils.CurrentSecond;
        }


        public void StartQuestTime()
        {
            data.hasQuest = true;
            data.questTime = new List<QuestTimeData>();
        }

        public void AddQuestTime(QuestTimeData questData)
        {
            data.questTime.Add(questData);
        }

        public void SaveLessonTimeData()
        {
            data.totalTime = TimeUtils.CurrentSecond - lessonTime.Value;
            DataUtils<LessonTimeData>.SaveData(Application.persistentDataPath + "/Data/Saved/test.txt", data);
        }
    }

    [Serializable]
    public class LessonTimeData
    {
        public long totalTime;
        public bool hasQuest;
        public List<QuestTimeData> questTime;
    }

    [Serializable]
    public class QuestTimeData
    {
        public int id;
        public string name;
        public long time;
    }
}

