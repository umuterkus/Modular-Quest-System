using System.Collections.Generic;
using UnityEngine;

public class ZoneManager : MonoBehaviour
{
    [SerializeField] private List<Item> itemsInZone = new List<Item>();

    [Header("Zone Identity")]
    [SerializeField] private ZoneIdentifierSO zoneID;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Item item))
        {
            if (!itemsInZone.Contains(item))
            {
                itemsInZone.Add(item);
                Debug.Log($"Item Entered: {item.name}");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Item item))
        {
            if (itemsInZone.Contains(item))
            {
                itemsInZone.Remove(item);
                Debug.Log($"Item Removed: {item.name}");
            }
        }
    }

    public void ProcessAndDeliverItems()
    {
        for (int i = itemsInZone.Count - 1; i >= 0; i--)
        {
            Item currentItem = itemsInZone[i];

            if (currentItem != null)
            {
                GameEvents.InvokeItemDelivery(currentItem.ItemData, zoneID);
                Debug.Log($"Item delivered from zone: {zoneID.name}");

                itemsInZone.RemoveAt(i);
                Destroy(currentItem.gameObject);
            }
        }
    }
}

