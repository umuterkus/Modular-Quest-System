using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestItemUI : MonoBehaviour
{
    [Header("Visual Elements")]
    [SerializeField] private TextMeshProUGUI zoneText;
    [SerializeField] private TextMeshProUGUI objectivesListText;

    public void SetupQuestItem(Quest quest)
    {
        zoneText.text = quest.GetZoneLocationText();

        UpdateProgressVisuals(quest);
    }

    public void UpdateProgressVisuals(Quest quest)
    {
        objectivesListText.text = quest.GetProgressText();
    }

    public void HandleCompletion()
    {
        objectivesListText.text = "<color=yellow>ALL OBJECTIVES COMPLETE!</color>";
        Destroy(gameObject, 2f);
    }
}