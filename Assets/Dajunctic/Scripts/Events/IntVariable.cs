using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dajunctic.Scripts.Events
{
    [CreateAssetMenu(fileName = "IntVariable", menuName = "Variables/IntVariable")]
    public class IntVariable : ScriptableObject
    {
        public int Value { get; set; }
    }
}