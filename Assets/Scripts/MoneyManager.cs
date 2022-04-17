using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyText;
    int currentMoney = 0;

    // Start is called before the first frame update
    void Start()
    {
        moneyText.text = currentMoney.ToString();
    }

    public void AddMoney(int value)
    {
        currentMoney += value;
        moneyText.text = currentMoney.ToString();

        moneyText.transform.DORewind();
        moneyText.transform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 0.1f);
    }
}
