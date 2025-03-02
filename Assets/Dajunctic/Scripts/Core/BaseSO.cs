using UnityEngine;

namespace Dajunctic.Scripts.Core
{
    public class BaseSO: ScriptableObject
    {
        [SerializeField] private string id;
        public string Id => id;

        public void ResetId()
        {
            id = name;
        }
    }
}