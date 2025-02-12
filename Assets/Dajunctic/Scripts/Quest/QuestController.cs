using System;
using System.Linq;
using Daark;
using UnityEngine;

namespace Dajunctic.Scripts.Quest
{
    public class QuestController: MonoBehaviour
    {
        [SerializeField] private Quest[] quests;
        [SerializeField] public QuestProgressUI questProgressUI;
        [SerializeField] public GameObject bubbleQuestion;
        [SerializeField] public GameObject congratulationUI;


        private int curQuestId;

        private void Awake()
        {
            foreach (var quest in quests)
            {
                quest.Init(this);
            }
            
            questProgressUI.gameObject.SetActive(false);
            bubbleQuestion.SetActive(false);
            congratulationUI.SetActive(false);

            curQuestId = 0;
        }

        private Quest GetCurQuest()
        {
            return quests.FirstOrDefault(x => x.Id == curQuestId);
        }

        private void Start()
        {
            var quest = GetCurQuest();
            quest?.SetState(Quest.State.Enable);
        }

        public void OnCompleteQuest()
        {
            if (curQuestId >= quests.Length - 1)
            {
                congratulationUI.SetActive(true);
                this.SendEvent(EventID.ExitScene);
                return;
            }
            
            curQuestId++;
            var quest = GetCurQuest();
            
            if (quest is null)
            {
                Debug.LogError($"Quest {curQuestId} not found in total {quests.Length} quests");
            }
            else
            {
                quest.SetState(Quest.State.Enable);
            }
        }

        
        
    }
}