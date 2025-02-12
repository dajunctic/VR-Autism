using System;
using KBCore.Refs;
using Plugins.QuickOutline.Scripts;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Dajunctic.Scripts.Quest
{
    public class Quest : MonoBehaviour
    {
        [Header("Setup quest")] 
        [SerializeField] private int id;
        [SerializeField] private QuestType questType;
        [ShowIf("@questType == QuestType.HoldClick || questType == QuestType.HoldTouch")]
        [SerializeField] private float duration;

        [Header("Components")] 
        [SerializeField] private Outline outline;
        [SerializeField] private Transform posBubbleQuestion;
        [SerializeField] private Transform posProgressBar;
        
        [Header("Events")]
        [SerializeField] private UnityEvent onQuestStarted;
        [SerializeField] private UnityEvent onQuestFinished;
        
        public int Id => id;
        private QuestController controller;
        private State state;
        private float progress;
        
        public enum State
        {
            Disable,
            Enable,
            Start,
            Completed
        }
        
        public void Init(QuestController questController)
        {
            controller = questController;
            state = State.Disable;
            if (outline) outline.enabled = false;
        }

        public void SetState(State newState)
        {
            state = newState;

            controller.bubbleQuestion.SetActive(state == State.Enable);
            controller.bubbleQuestion.transform.position = posBubbleQuestion.position;
            controller.questProgressUI.gameObject.SetActive(state == State.Start);
            controller.questProgressUI.transform.position = posProgressBar.position;
            if (outline) outline.enabled = newState == State.Start;

            if (state == State.Start)
            {
                progress = 0;
                onQuestStarted?.Invoke();
            }
            if (state == State.Completed)
            {
                onQuestFinished?.Invoke();
                controller.OnCompleteQuest();
            }
        }
        

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Character") || state == State.Disable) return;

            if (state == State.Enable)
            {
                if (questType == QuestType.Touch)
                {
                    SetState(State.Completed);
                }

                if (questType == QuestType.HoldTouch)
                {
                    SetState(State.Start);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Character") || state == State.Disable) return;

            if (state != State.Completed)
            {
                SetState(State.Enable);
            }
        }

        private void Update()
        {
            if (state != State.Start) return;
            
            progress += Time.deltaTime / duration;
            controller.questProgressUI.SetProgress(progress);
            if (progress >= 1)
            {
                progress = 1;
                SetState(State.Completed);
            }
        }
    }

    public enum QuestType
    {
        Click,
        Touch,
        HoldClick,
        HoldTouch
    }
}


