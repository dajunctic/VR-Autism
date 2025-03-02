using UnityEngine;

namespace Dajunctic.Scripts.Events
{
    [CreateAssetMenu(fileName = "DoubleVariable", menuName = "Variables/DoubleVariable")]
    public class DoubleVariable : ScriptableObject
    {
        public double Value { get; set; }
    }
}

