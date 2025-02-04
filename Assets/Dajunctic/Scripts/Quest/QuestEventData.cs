using System;
using Dajunctic.Scripts.Core;

namespace Dajunctic.Scripts.Quest
{
    public class QuestEventData: BaseSO
    {
        public Action<Quest> OnQuestCompleted;

        public void OnCompleteQuest(Quest quest)
        {
            OnQuestCompleted?.Invoke(quest);
        }
    }
}