using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    List<Item> inventoryItems = new List<Item>();

    MoneyManager moneyManager;
    AudioSource audioSource;

    [SerializeField] AudioClip sellSound;

    [SerializeField] GameObject toastTextPrefab;
    [SerializeField] Transform toastCanvas;

    void Start()
    {
        moneyManager = FindObjectOfType<MoneyManager>();
        audioSource = GetComponent<AudioSource>();
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
            audioSource.PlayOneShot(sellSound, 0.4f);

            GameObject temp = Instantiate(toastTextPrefab, toastCanvas.position, toastCanvas.rotation);
            temp.transform.SetParent(toastCanvas);
            temp.transform.localPosition = new Vector3(0, -875, 0);
            toastTextPrefab.GetComponent<ToastText>().SetText(item.itemName + " sold");
            moneyManager.AddMoney(item.itemPrice);
            yield return new WaitForSeconds(0.35f);
        }

        ClearInventory();
    }
}
