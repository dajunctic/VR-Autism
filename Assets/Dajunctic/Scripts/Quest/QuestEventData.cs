using System;
using Dajunctic.Scripts.Core;

namespace Dajunctic.Scripts.Quest
{
    public class QuestEventData: BaseSO
    {
        public Action<object> OnQuestFinished;
    }
}