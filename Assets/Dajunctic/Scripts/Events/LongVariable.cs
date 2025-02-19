using UnityEngine;

namespace Dajunctic.Scripts.Events
{
    [CreateAssetMenu(fileName = "LongVariable", menuName = "Variables/LongVariable")]
    public class LongVariable : ScriptableObject
    {
        public long Value { get; set; }
    }
}

