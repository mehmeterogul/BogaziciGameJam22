using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    List<Item> inventoryItems = new List<Item>();

    public void AddItem(List<Item> items)
    {
        if(items.Count > 1)
        {
            foreach (Item item in items)
            {
                inventoryItems.Add(item);
            }
        }
    }
}
