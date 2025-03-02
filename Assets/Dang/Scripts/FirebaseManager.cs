using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;
using System.IO;
using Dajunctic.Scripts.Manager;

public class FirebaseManager : MonoBehaviour
{
    private DatabaseReference dbReference;

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            if (task.Result == DependencyStatus.Available)
            {
                FirebaseApp app = FirebaseApp.DefaultInstance;
                FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(true);
                dbReference = FirebaseDatabase.DefaultInstance.RootReference;
                Debug.Log("Firebase initialized successfully and offline persistence enabled!");
            }
            else
            {
                Debug.LogError($"Failed to initialize Firebase: {task.Result}");
            }
        });
    }

   
    public void UploadLessonTimeData()
    {
        string filePath = Application.persistentDataPath + "/Data/Saved/test.txt";

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            LessonTimeData data = JsonUtility.FromJson<LessonTimeData>(json);

            if (data != null)
            {
                string key = dbReference.Child("lessonTimeData").Push().Key;

                dbReference.Child("lessonTimeData").Child(key).SetRawJsonValueAsync(json).ContinueWithOnMainThread(task => {
                    if (task.IsCompleted)
                    {
                        Debug.Log("Lesson time data uploaded successfully.");
                    }
                    else
                    {
                        Debug.LogError("Failed to upload lesson time data: " + task.Exception);
                    }
                });
            }
            else
            {
                Debug.LogError("Failed to parse JSON from test.txt.");
            }
        }
        else
        {
            Debug.LogError("test.txt not found at: " + filePath);
        }
    }
}
