using System;
using KBCore.Refs;
using Plugins.QuickOutline.Scripts;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Dajunctic.Scripts.Quest
{
    public class Quest : MonoBehaviour
    {
        [Header("Setup quest")] 
        [SerializeField] private int id;
        [SerializeField] private QuestType questType;
        [ShowIf("@questType == QuestType.HoldClick || @questType == QuestType.HoldTouch)]")]
        [SerializeField] private float duration;

        [Header("Components")] 
        [SerializeField, Child] private Outline outline;
        [SerializeField] private GameObject bubbleQuestion;
        [SerializeField] private QuestProgressUI questProgressUI;
        [SerializeField] private GameObject posBubbleQuestion;
        [SerializeField] private GameObject posProgressBar;
        
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
        }

        public void SetState(State newState)
        {
            state = newState;

            bubbleQuestion.SetActive(state == State.Enable);
            outline.enabled = newState == State.Start;

            if (state == State.Start)
            {
                progress = 0;
            }
            if (state == State.Completed)
            {
                controller.OnCompleteQuest();
            }
        }
        

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player") || state == State.Disable) return;

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
            if (!other.CompareTag("Player") || state == State.Disable) return;

            if (state != State.Completed)
            {
                SetState(State.Enable);
            }
        }

        private void Update()
        {
            if (state != State.Start) return;
            
            progress += Time.deltaTime / duration;
            questProgressUI.SetProgress(progress);
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


