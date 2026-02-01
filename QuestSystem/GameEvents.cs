using System;

public static class GameEvents
{

    // QUEST MANAGER

    public static event Action<ItemData, ZoneIdentifierSO> OnItemDelivered;
    public static event Action<Quest> OnQuestCompleted;

    public static void InvokeItemDelivery(ItemData item, ZoneIdentifierSO zoneID)
        => OnItemDelivered?.Invoke(item, zoneID);

    public static void InvokeQuestCompleted(Quest quest)
        => OnQuestCompleted?.Invoke(quest);

    // QUEST MANAGER UI

    public static event Action<Quest> OnQuestStarted;
    public static event Action<Quest> OnQuestProgressUpdated;

    public static void InvokeQuestStarted(Quest quest) => OnQuestStarted?.Invoke(quest);
    public static void InvokeQuestProgressUpdated(Quest quest) => OnQuestProgressUpdated?.Invoke(quest);

}
