using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[System.Serializable]
public class QuestObjective
{
    public ItemData Item;
    protected int _currentAmount = 0;
    public int RequiredAmount { get; protected set; }
    public int CurrentAmount
    {
        get { return _currentAmount; }
        set
        {
            if (value > RequiredAmount)
            {
                _currentAmount = RequiredAmount;
            }
            else
            {
                _currentAmount = value;
            }
        }
    }

    public bool IsCompleted => CurrentAmount >= RequiredAmount;

    public QuestObjective(ItemDataSO item, int amount)
    {
        Item = item.GetItemData();
        RequiredAmount = amount;
        CurrentAmount = 0;
    }
}
// public class UniqueQuestObjective : QuestObjective
// {
//     public UniqueQuestObjective(ItemData item)
//     {
//         Item = item;
//         RequiredAmount = 1;
//         CurrentAmount = 0;
//     }
// }

[System.Serializable]
public class Quest
{
    public string QuestName { get; private set; }
    public bool IsCompleted { get; private set; }

    public List<QuestObjective> Objectives { get; private set; }

    private ZoneIdentifierSO SelectedZone;

    public Quest(string name, List<QuestObjective> objectives, ZoneIdentifierSO zone)
    {
        QuestName = name;
        Objectives = objectives;
        SelectedZone = zone;
        IsCompleted = false;
    }
    public bool EvaluateProgress(ItemData item, ZoneIdentifierSO zoneID)
    {
        if (IsCompleted) return false;

        if (SelectedZone != null && SelectedZone != zoneID) return false;

        var matchingObjective = Objectives.FirstOrDefault(obj => obj.Item == item && !obj.IsCompleted);

        if (matchingObjective == null) return false;

        matchingObjective.CurrentAmount++;


        Debug.Log($"Mission update [{QuestName}]: {item.Name} -> {matchingObjective.CurrentAmount}/{matchingObjective.RequiredAmount}");

        GameEvents.InvokeQuestProgressUpdated(this);

        if (Objectives.All(obj => obj.IsCompleted))
        {
            Complete();
        }

        return true;
    }

    public string GetProgressText()
    {
        string text = "";
        foreach (var obj in Objectives)
        {

            string color = obj.IsCompleted ? "green" : "white";
            text += $"<color={color}>{obj.Item.Name}: {obj.CurrentAmount}/{obj.RequiredAmount}</color>\n";
        }
        return text;
    }

    public string GetZoneLocationText()
    {
        return SelectedZone == null ? "Any Zone" : SelectedZone.DisplayName;
    }

    private void Complete()
    {
        IsCompleted = true;
        GameEvents.InvokeQuestCompleted(this);
    }
}
