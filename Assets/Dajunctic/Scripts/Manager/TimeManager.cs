using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Dajunctic.Scripts.Events;
using Dajunctic.Scripts.Utils;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;
using System.IO;

namespace Dajunctic.Scripts.Manager
{
    public class TimeManager : MonoBehaviour
    {
        [SerializeField] private DoubleVariable lessonTime;
        [SerializeField] private FirebaseManager firebaseManager;
        [SerializeField] private VideoRecorder videoRecorder;
        [SerializeField] private GoogleDriveUploader uploader;
        private Stopwatch timer;
        
        private LessonTimeData data;

        private DateTime startTime; 
        private DateTime endTime;
        


        private void Awake()
        {
            data = new LessonTimeData();
        }

        public void StartLessonTime()
        {
            startTime = DateTime.Now;
            videoRecorder.StartRecording();
            //data.SetStartTime(DateTime.Now);
            timer = new Stopwatch();
            timer.Start();
            UnityEngine.Debug.Log("Lesson started at: " + startTime.ToString("yyyy-MM-dd HH:mm:ss"));
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
            timer.Stop();
            endTime = DateTime.Now;
            data.startTime = startTime.ToString("yyyy-MM-ddTHH:mm:ss");
            data.endTime = endTime.ToString("yyyy-MM-ddTHH:mm:ss");
            data.totalTime = timer.Elapsed.TotalMilliseconds;
            videoRecorder.StopRecording();
            string videoPath = videoRecorder.GetVideoPath();

            StartCoroutine(uploader.UploadVideo(videoPath, (fileId) =>
            {
                UnityEngine.Debug.Log("Start upload video");
                /*firebaseManager.SaveVideoUrlToFirebase("student_001", "WashingHand", fileId);*/
                data.videoUrl = "https://drive.google.com/file/d/" + fileId + "/preview";
                DataUtils<LessonTimeData>.SaveData(Application.persistentDataPath + "/Data/Saved/test.txt", data);
                firebaseManager.UploadLessonTimeData();
            }));

            //data.totalTime = TimeUtils.CurrentSecond - lessonTime.Value;
            
            //string filePath = Application.persistentDataPath + "/Data/Saved/test.txt";
            //File.WriteAllText(filePath, JsonUtility.ToJson(data, true));

            //firebaseManager.UploadLessonTimeData();

        }
    }

    [Serializable]
    public class LessonTimeData
    {
        public double totalTime;
        public bool hasQuest;
        public String startTime;
        public String endTime;
        public String videoUrl;
        public List<QuestTimeData> questTime;
    }

    [Serializable]
    public class QuestTimeData
    {
        public int id;
        public string name;
        public double time;
        public int hintCount;
    }
}

