using System;
using Daark;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Dajunctic.Scripts.Objects
{
    public class FaucetFlowController : MonoBehaviour
    {
        [SerializeField, ChildGameObjectsOnly] private Animator animator;
        private bool turnOn;

        private void Awake()
        {
            this.SubscribeListener(EventID.ToggleFaucet, param => Toggle((bool)param));
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!turnOn)
                {
                    animator.Play("FaucetWaterStart");
                }
                else
                {
                    animator.Play("FaucetWaterEnd");
                }
            }
        }

        public void TurnOn()
        {
            if (!turnOn)
            {
                animator.Play("FaucetWaterStart");
            }
        }

        public void TurnOff()
        {
            animator.Play("FaucetWaterEnd");
        }

        private void Toggle(bool state)
        {
            turnOn = state;
        }
    }
}
