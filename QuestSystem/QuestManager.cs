using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private QuestTemplateSO questTemplateSO;

    [SerializeField] private int maxQuestAmount = 5;
    [SerializeField] private float generationInterval = 5f;
    [SerializeField, Range(0f, 1f)] private float newQuestChance = 0.3f;

    [Header("Debug View")]
    [SerializeField] private List<Quest> activeQuests = new();

    private void Start()
    {
        for (int i = 0; i < 1; i++)
        {
            GenerateNewQuest(force: true);
        }
        StartCoroutine(QuestGenerationRoutine());
    }

    private void OnEnable()
    {
        GameEvents.OnQuestCompleted += HandleQuestCompleted;
        GameEvents.OnItemDelivered += HandleItemTransaction;
    }

    private void OnDisable()
    {

        GameEvents.OnQuestCompleted -= HandleQuestCompleted;
        GameEvents.OnItemDelivered -= HandleItemTransaction;
    }


    private void HandleItemTransaction(ItemData item, ZoneIdentifierSO zoneID)
    {
        if (activeQuests.Count == 0) return;

        foreach (Quest quest in activeQuests.ToList())
        {
            if (quest.EvaluateProgress(item, zoneID))
            {
                break;
            }
        }
    }

    private void HandleQuestCompleted(Quest quest)
    {
        Debug.Log($"<color=green>Completed: {quest.QuestName}</color>");

        if (activeQuests.Contains(quest))
        {
            activeQuests.Remove(quest);
        }
    }

    private void GenerateNewQuest(bool force = false)
    {
        if (activeQuests.Count >= maxQuestAmount) return;
        if (!force && Random.value > newQuestChance) return;
        if (questTemplateSO == null) return;

        Quest newQuest = questTemplateSO.CreateQuest();

        if (newQuest != null)
        {
            activeQuests.Add(newQuest);

            GameEvents.InvokeQuestStarted(newQuest);

            Debug.Log($"<color=cyan>New Mission Added: {newQuest.QuestName}</color>");
        }
    }

    private IEnumerator QuestGenerationRoutine()
    {
        while (true)
        {
            GenerateNewQuest();
            yield return new WaitForSeconds(generationInterval);
        }
    }
}
