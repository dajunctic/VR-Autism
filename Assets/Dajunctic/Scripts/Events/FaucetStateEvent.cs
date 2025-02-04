using Daark;
using UnityEngine;

namespace Dajunctic.Scripts.Events
{
    public class FaucetStateEvent : MonoBehaviour
    {
        public void Enable()
        {
            this.SendEvent(EventID.ToggleFaucet, true);
        }

        public void Disable()
        {
            this.SendEvent(EventID.ToggleFaucet, false);

        }
    }
}

