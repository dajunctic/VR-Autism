using UnityEngine;

[System.Serializable]
public class Lesson
{
    public int ID;                 
    public string Title;            
    public string Description;     
    public Sprite Cover;            

 
    public Lesson(int id, string title, string description, Sprite cover)
    {
        ID = id;
        Title = title;
        Description = description;
        Cover = cover;
    }
}

