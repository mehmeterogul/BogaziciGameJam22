using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    List<Item> inventoryItems = new List<Item>();

    MoneyManager moneyManager;

    [SerializeField] GameObject toastTextPrefab;
    [SerializeField] Transform gameCanvas;

    void Start()
    {
        moneyManager = FindObjectOfType<MoneyManager>();
    }

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

    public List<Item> GetItems()
    {
        return inventoryItems;
    }

    void ClearInventory()
    {
        inventoryItems.Clear();
    }

    public void SellAll()
    {
        StartCoroutine("SellCoroutine");
    }

    IEnumerator SellCoroutine()
    {
        foreach (Item item in inventoryItems)
        {
            GameObject temp = Instantiate(toastTextPrefab, gameCanvas.position, gameCanvas.rotation);
            temp.transform.SetParent(gameCanvas);
            temp.transform.localPosition = new Vector3(-540, -960, 0);
            toastTextPrefab.GetComponent<ToastText>().SetText(item.itemName + " has sold");
            yield return new WaitForSeconds(0.5f);
            moneyManager.AddMoney(item.itemPrice);
        }

        ClearInventory();
    }
}
