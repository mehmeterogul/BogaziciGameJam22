using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    public int itemPrice;

    // Start is called before the first frame update
    void Start()
    {
        itemName = transform.name;
    }
}
