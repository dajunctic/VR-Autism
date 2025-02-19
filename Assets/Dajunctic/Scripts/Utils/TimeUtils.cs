using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dajunctic.Scripts.Utils
{
    public static class TimeUtils
    {
        public static long CurrentSecond => ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds();
        public static long CurrentDay => CurrentSecond / 3600;
    }

}
