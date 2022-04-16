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
        StartPanelAnimation();
    }

    void StartPanelAnimation()
    {
        playerCanvas.transform.DOLocalMove(new Vector3(0, 10.2f, -6.88f), 1f);
    }
}
