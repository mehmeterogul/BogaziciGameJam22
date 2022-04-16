using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerRobbingManager : MonoBehaviour
{
    [SerializeField] Image panel;
    [SerializeField] GameObject bag;

    public void StartRobbing(BagContent bagContent, int npcTypeIndex, string npcMaterialName)
    {
        Debug.Log(npcTypeIndex);
        Debug.Log(npcMaterialName);

        StartBagPasswordStateAnimation();
    }

    void StartBagPasswordStateAnimation()
    {

    }
}
