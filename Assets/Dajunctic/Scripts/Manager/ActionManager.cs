using System;
using System.Collections;
using System.Collections.Generic;
using Dajunctic.Scripts.Events;
using UnityEngine;
using UnityEngine.Events;

namespace Dajunctic.Scripts.Manager
{
    public class ActionManager : MonoBehaviour
    {
        [SerializeField] private List<ActionEvent> actionEvents;
        
        private void Start()
        {
            StartCoroutine(ActionLoop());
        }

        private IEnumerator ActionLoop()
        {
            foreach (var actionEvent in actionEvents)
            {
                if (!actionEvent.on) continue;
                Debug.Log("[Debug] <color=#00ff48>Event </color> <color=#ffea00>" + actionEvent.name + "</color> is starting...");
                actionEvent.onStart?.Invoke();
                yield return new WaitForSeconds(actionEvent.duration);
                if (actionEvent.isConditionMet is not null)
                {
                    yield return new WaitUntil(() => actionEvent.isConditionMet.Value);
                }
                actionEvent.onFinished?.Invoke();
            }
            
            Debug.Log("[Debug] <color=#00ff48>All actions have been finished...</color>");

        }
    }

    [Serializable]
    public class ActionEvent
    {
        public string name;
        public bool on;
        public float duration;
        public UnityEvent onStart;
        public UnityEvent onFinished;
        public BooleanVariable isConditionMet;
    }
}

