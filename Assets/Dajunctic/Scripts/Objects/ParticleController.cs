using UnityEngine;

namespace Dajunctic.Scripts.Objects
{
    public class ParticleController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particle;

        public void PlayParticle()
        {
            if (particle != null)
            {
                particle.gameObject.SetActive(true);
                particle.Play();
            }
                
        }

        public void StopParticle()
        {
            if (particle != null)
                particle.Stop();
        }
    }
}
