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
        public TypeSound introductionSound;
        public TypeSound descriptionSound;
        public float timeDescriptions;
    }
    
    public float timeIntroToSound=4f;
    public float timeSoundToDescription=4f;
    public float timeToNextAnimal = 2f;
    

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
        yield return new WaitForSeconds(2f);
        this.SendEvent(EventID.PlaySound, TypeSound.WelcomeToAnimalLesson);
        yield return new WaitForSeconds(5f);

        for (var i = 0; i < animals.Length; i++)
        {
            var animal = animals[i];
            
            yield return MoveCameraToAnimal(animal.cameraPos);
            this.SendEvent(EventID.PlaySound, animal.introductionSound);
            yield return new WaitForSeconds(timeIntroToSound);
            this.SendEvent(EventID.PlaySound, animal.sound);
            yield return new WaitForSeconds(timeSoundToDescription);
            this.SendEvent(EventID.PlaySound, animal.descriptionSound);
            yield return new WaitForSeconds(animal.timeDescriptions);

            if (i != animals.Length - 1)
            {
                this.SendEvent(EventID.PlaySound, TypeSound.NextAnimal);
                yield return new WaitForSeconds(timeToNextAnimal);
            }
        }
        
        yield return new WaitForSeconds(5f);
        this.SendEvent(EventID.PlaySound, TypeSound.ThankParticipateLesson);
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
    Rabbit,
    Horse,
    Bear,
    Lion,
    Elephant,
    Giraffe,
}
