using System.Collections.Generic;
using TMPro; 
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject questItemPrefab;
    [SerializeField] private Transform contentContainer;

    private Dictionary<Quest, QuestItemUI> questItems = new Dictionary<Quest, QuestItemUI>();

    private void OnEnable()
    {
        GameEvents.OnQuestStarted += HandleQuestStarted;
        GameEvents.OnQuestProgressUpdated += HandleQuestProgress;
        GameEvents.OnQuestCompleted += HandleQuestCompleted;
    }

    private void OnDisable()
    {
        GameEvents.OnQuestStarted -= HandleQuestStarted;
        GameEvents.OnQuestProgressUpdated -= HandleQuestProgress;
        GameEvents.OnQuestCompleted -= HandleQuestCompleted;
    }

    private void HandleQuestStarted(Quest quest)
    {
        GameObject newQuest = Instantiate(questItemPrefab, contentContainer);

        QuestItemUI itemUI = newQuest.GetComponent<QuestItemUI>();

        itemUI.SetupQuestItem(quest);
     
        questItems.Add(quest, itemUI);
    }

    private void HandleQuestProgress(Quest quest)
    {
      
        if (questItems.TryGetValue(quest, out QuestItemUI itemUI))
        {
            itemUI.UpdateProgressVisuals(quest);
        }
    }

    private void HandleQuestCompleted(Quest quest)
    {
        if (questItems.TryGetValue(quest, out QuestItemUI itemUI))
        {
            itemUI.HandleCompletion();

            questItems.Remove(quest);
        }
    }
}