using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerRobbingManager : MonoBehaviour
{
    [SerializeField] GameObject playerCanvas;
    [SerializeField] GameObject bag;

    public void StartRobbing(BagContent bagContent, int npcTypeIndex, string npcMaterialName)
    {
        Debug.Log(npcTypeIndex);
        Debug.Log(npcMaterialName);

        StartBagPasswordStateAnimation();
    }

    void StartBagPasswordStateAnimation()
    {
        PanelAnimation();
        Invoke("BagAnimation", 1f);
    }

    void PanelAnimation()
    {
        playerCanvas.transform.DOLocalMove(new Vector3(0, 10.2f, -6.88f), 1f);
    }
    
    void BagAnimation()
    {
        bag.transform.DOLocalMove(new Vector3(0, 0.075f, 0.84f), 0.5f);
    }
}
