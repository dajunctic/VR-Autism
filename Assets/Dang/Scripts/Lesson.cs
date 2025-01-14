using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Topic
{
    public int id;
    public string name;
    public List<Lesson> lessons;
}

[Serializable]
public class Lesson
{
    public int id;
    public int topicId;
    public string sceneName;
    public string title;            
    public string description;     
    public Sprite cover;
    public List<float> sessionTimes;
}


