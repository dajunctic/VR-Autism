using System;
using System.Linq;
using Daark;
using Dajunctic.Scripts.Events;
using Dajunctic.Scripts.Manager;
using Dajunctic.Scripts.Utils;
using UnityEngine;

namespace Dajunctic.Scripts.Quest
{
    public class QuestController: MonoBehaviour
    {
        [SerializeField] private TimeManager timeManager;
        [SerializeField] private Quest[] quests;
        [SerializeField] public QuestProgressUI questProgressUI;
        [SerializeField] public GameObject bubbleQuestion;
        [SerializeField] public GameObject congratulationUI;
        [SerializeField] private BooleanVariable isConditionMet;
        [SerializeField] private DoubleVariable timeVariable;


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

        public void StartRunningQuest()
        {
            isConditionMet.Value = false;
            StartNewQuest();
        }

        public void OnCompleteQuest()
        {
            if (timeManager)
            {
                var finishedTime = TimeUtils.CurrentSecond - timeVariable.Value;
                
                timeManager.AddQuestTime(
                    new QuestTimeData
                    {
                        id = curQuestId,
                        name = GetCurQuest().Name,
                        time = finishedTime,
                    });
            }
            
            if (curQuestId >= quests.Length - 1)
            {
                congratulationUI.SetActive(true);
                this.SendEvent(EventID.ExitScene);
                isConditionMet.Value = true;
                return;
            }
            
            curQuestId++;
            StartNewQuest();
        }


        private void StartNewQuest()
        {
            timeVariable.Value = TimeUtils.CurrentSecond;
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