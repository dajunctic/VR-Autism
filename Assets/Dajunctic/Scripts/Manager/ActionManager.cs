using System;
using System.Collections;
using System.Collections.Generic;
using Dajunctic.Scripts.Events;
using Dajunctic.Scripts.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace Dajunctic.Scripts.Manager
{
    public class ActionManager : MonoBehaviour
    {
        public static ActionManager Instance;
        [SerializeField] private IntVariable hintCount;
        [SerializeField] private List<ActionEvent> actionEvents;

        private double startTime;
        private double endTime;
        private int index;

        void Awake()
        {
            Instance = this;
        }
        
        private void Start()
        {
            StartCoroutine(ActionLoop());
            index = 0;
        }

        public List<string> GetQuestName()
        {
            var result = new List<string>();
            foreach (var action in actionEvents)
            {
                if (action.onSendData) result.Add(action.name);
            }
            return result;
        }

        private IEnumerator ActionLoop()
        {
            foreach (var actionEvent in actionEvents)
            {
                if (!actionEvent.on) continue;
                Debug.Log("[Debug] <color=#00ff48>Event </color> <color=#ffea00>" + actionEvent.name + "</color> is starting...");
                actionEvent.onStart?.Invoke();
                startTime = TimeUtils.CurrentSecond;
                hintCount.Value = 0;
                
                yield return new WaitForSeconds(actionEvent.duration);
                if (actionEvent.isConditionMet is not null)
                {
                    yield return new WaitUntil(() => actionEvent.isConditionMet.Value);
                }
                actionEvent.onFinished?.Invoke();
                endTime = TimeUtils.CurrentSecond;

                if (actionEvent.onSendData)
                {
                    if (FirebaseManager.Instance is null)
                    {
                        Debug.LogError("Firebase is null!");
                    }
                    else
                    {
                        FirebaseManager.Instance.UpdateQuestData("response_time", endTime - startTime, index);
                        FirebaseManager.Instance.UpdateQuestData("hint_count", hintCount.Value, index);
                    }

                    index++;
                }

                if (TimeManager.Instance is null)
                {
                    Debug.LogError("TimeManager is null!");
                }
                else
                {
                    TimeManager.Instance.SaveDurationTime();
                }
            }
            
            Debug.Log("[Debug] <color=#00ff48>All actions have been finished...</color>");

        }
    }

    [Serializable]
    public class ActionEvent
    {
        public string name;
        public bool on;
        public bool onSendData;
        public float duration;
        public UnityEvent onStart;
        public UnityEvent onFinished;
        public BooleanVariable isConditionMet;
    }
}

