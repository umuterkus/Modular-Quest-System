using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "QuestSystem/Master Quest Template")]
public class QuestTemplateSO : ScriptableObject
{
    public string QuestTitleFormat = "";

    [Header("Rules")]
    public Vector2Int AmountRange = new Vector2Int(1, 5); 
    public Vector2Int ObjectiveCountRange = new Vector2Int(1, 3); 

    [Header("Zone Configurations")]
    public List<ZoneQuestConfig> ZoneConfigurations;

    [System.Serializable]
    public class ZoneQuestConfig
    {
        public string ZoneName; 
        public ZoneIdentifierSO Zone;
        public List<ItemDataSO> PossibleItems;
    }

    public Quest CreateQuest()
    {
        if (ZoneConfigurations == null || ZoneConfigurations.Count == 0)
        {
            return null;
        }

        var config = ZoneConfigurations[Random.Range(0, ZoneConfigurations.Count)];

        if (config.PossibleItems == null || config.PossibleItems.Count == 0)
        {
            return null;
        }
        List<ItemDataSO> itemPool = new List<ItemDataSO>(config.PossibleItems);
        ZoneIdentifierSO targetZone = config.Zone;


        int targetObjectiveCount = Random.Range(ObjectiveCountRange.x, ObjectiveCountRange.y + 1);
        targetObjectiveCount = Mathf.Min(targetObjectiveCount, itemPool.Count);

        List<QuestObjective> objectives = new List<QuestObjective>();

        for (int i = 0; i < targetObjectiveCount; i++)
        {
            int randomIndex = Random.Range(0, itemPool.Count);

            ItemDataSO selectedItem = itemPool[randomIndex];
            int amount = Random.Range(AmountRange.x, AmountRange.y + 1);

            objectives.Add(new QuestObjective(selectedItem, amount));

            itemPool.RemoveAt(randomIndex);
        }

        return new Quest(QuestTitleFormat, objectives, targetZone);
    }
}