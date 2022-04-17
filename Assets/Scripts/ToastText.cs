using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToastText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI toastText;

    void Start()
    {
        Destroy(gameObject, 2f);
    }

    public void SetText(string text)
    {
        toastText.text = text;
    }
}
