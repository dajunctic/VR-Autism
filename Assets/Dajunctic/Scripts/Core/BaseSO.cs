using Sirenix.OdinInspector;
using UnityEngine;

namespace Dajunctic.Scripts.Core
{
    public class BaseSO: ScriptableObject
    {
        [ReadOnly, SerializeField] private string id;
        public string Id => id;

        [Button]
        public void ResetId()
        {
            id = name;
        }
    }
}