using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dajunctic.Scripts.Events
{
    [CreateAssetMenu(fileName = "BooleanVariable", menuName = "Variables/BooleanVariable")]
    public class BooleanVariable : ScriptableObject
    {
       public bool Value { get; set; }
    }
}

