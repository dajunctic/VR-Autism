using KBCore.Refs;
using UnityEngine;

namespace Dajunctic.Scripts.Objects
{
    public class DispenserController : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        public void Push()
        {
            animator.SetTrigger("Push");
        }
    }

}
