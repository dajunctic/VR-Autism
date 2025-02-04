using System;
using System.Linq;
using UnityEngine;

namespace Dajunctic.Scripts.Quest
{
    public class QuestController: MonoBehaviour
    {
        [SerializeField] private Quest[] quests;

        private int curQuestId;

        private void Awake()
        {
            foreach (var quest in quests)
            {
                quest.Init(this);
            }

            curQuestId = 0;
        }

        private Quest GetCurQuest()
        {
            return quests.First(x => x.Id == curQuestId);
        }

        private void Start()
        {
            var quest = GetCurQuest();

            if (quest != null)
            {
                quest.SetState(Quest.State.Enable);
            }
        }

        public void OnCompleteQuest()
        {
            curQuestId++;
            var quest = GetCurQuest();
            
            if (curQuestId >= quests.Length || quest is null)
            {
                Debug.LogError($"Quest {curQuestId} not found in total {quests.Length} quests");
            }
            else
            {
                
            }
        }

        
        
    }
}