using UnityEngine;

[CreateAssetMenu(menuName = "QuestSystem/New Zone Identifier Type")]
public class ZoneIdentifierSO : ScriptableObject
{
    [Header("Settings")]
    public string DisplayName;
    [TextArea] public string Description;
}