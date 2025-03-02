using UnityEngine;

namespace Dajunctic.Scripts.Events
{
    [CreateAssetMenu(fileName = "FloatVariable", menuName = "Variables/FloatVariable")]
    public class FloatVariable : ScriptableObject
    {
        public float Value { get; set; }
    }
}

