using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRandomizer : MonoBehaviour
{
    void SetChildsActiveFalse()
    {
        foreach (Transform child in transform)
        {
            foreach (Transform grandChild in child)
            {
                grandChild.gameObject.SetActive(false);
            }
        }
    }

    public void Randomize()
    {
        SetChildsActiveFalse();

        foreach (Transform child in transform)
        {
            int grandChildCount = transform.childCount;
            int randomGrandChildIndex = Random.Range(0, grandChildCount);

            child.GetChild(randomGrandChildIndex).gameObject.SetActive(true);
        }
    }
}
