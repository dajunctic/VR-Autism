using System.Collections;
using Daark;
using UnityEngine;

public class AnimalLessonManager : MonoBehaviour
{
    [System.Serializable]
    public class Animal
    {
        public AnimalType name;
        public Transform cameraPos;
        public TypeSound sound;
        public TypeSound descriptionSound;
        public float timeDescriptions;
    }
    
    public float timeSoundToDescription=4f;

    public TypeSound backgroundMusic;
    public TypeSound introSound;
    public TypeSound endSound;
    public float timeIntroSound;
    public Animal[] animals; 
    public Camera mainCamera;
    public float cameraMoveSpeed = 2f;
    
    private void Start()
    {
        StartLesson();
    }

    private void StartLesson()
    {
        StartCoroutine(IELesson());
    }

    private IEnumerator IELesson()
    {
        this.SendEvent(EventID.PlaySound, backgroundMusic); 
        yield return new WaitForSeconds(1f);
        this.SendEvent(EventID.PlaySound, introSound);
        yield return new WaitForSeconds(timeIntroSound);

        for (var i = 0; i < animals.Length; i++)
        {
            var animal = animals[i];
            
            yield return MoveCameraToAnimal(animal.cameraPos);
            this.SendEvent(EventID.PlaySound, animal.descriptionSound);
            yield return new WaitForSeconds(animal.timeDescriptions);
            this.SendEvent(EventID.PlaySound, animal.sound);
            yield return new WaitForSeconds(timeSoundToDescription);
        }
        
        yield return new WaitForSeconds(1f);
        this.SendEvent(EventID.PlaySound, endSound);
    }

    private IEnumerator MoveCameraToAnimal(Transform animalModel)
    {
        Vector3 targetPosition = animalModel.position; 
        Quaternion targetRotation = animalModel.rotation;

        while (Vector3.Distance(mainCamera.transform.position, targetPosition) > 0.1f)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, Time.deltaTime * cameraMoveSpeed);
            mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, targetRotation, Time.deltaTime * cameraMoveSpeed);
            yield return null;
        }
    }
}

public enum AnimalType
{
    // Grass Land
    Rabbit,
    Zebra,
    Lion,
    Elephant,
    Giraffe,
    // Farm
    Chicken,
    Sheep,
    Dog,
    Pig,
    Cow,
    // Ocean
    Shark,
    Jellyfish,
    Dolphin,
    Turtle
}
