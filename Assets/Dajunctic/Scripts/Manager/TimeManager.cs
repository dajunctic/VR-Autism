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
        [SerializeField] private DoubleVariable lessonTime;
        
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

            // FindObjectOfType<FirebaseManager>().UploadLessonTimeData();
        }
    }

    [Serializable]
    public class LessonTimeData
    {
        public double totalTime;
        public bool hasQuest;
        public List<QuestTimeData> questTime;
    }

    [Serializable]
    public class QuestTimeData
    {
        public int id;
        public string name;
        public double time;
    }
}

