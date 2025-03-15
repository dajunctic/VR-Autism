/*using Firebase;
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
*/

using System;
using System.IO;
using System.Threading.Tasks;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;

public class FirebaseManager : MonoBehaviour
{
    private DatabaseReference dbReference;

    private void Awake()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        });
    }

    public void UploadLessonTimeData()
    {
        string filePath = Application.persistentDataPath + "/Data/Saved/test.txt";

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            SaveJsonToFirebase(json);
        }
        else
        {
            Debug.LogError("File không tồn tại: " + filePath);
        }
    }

    private void SaveJsonToFirebase(string jsonData)
    {
        dbReference.Child("students")
            .Child("student_001")
            .Child("lessons")
            .Child("WashingHand")
            .Child("levels")
            .Child("1")
            .Child("sessions")
            .Push() 
            .SetRawJsonValueAsync(jsonData)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("Dữ liệu mới đã được thêm vào Firebase thành công!");
                }
                else
                {
                    Debug.LogError("Lỗi khi thêm dữ liệu vào Firebase: " + task.Exception);
                }
            });
    }


}
