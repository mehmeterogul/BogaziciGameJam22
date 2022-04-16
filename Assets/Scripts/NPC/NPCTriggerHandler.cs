using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCTriggerHandler : MonoBehaviour
{
    [SerializeField] Image circleSprite;
    [SerializeField] float fillRate;
    [SerializeField] float maxFillValue = 100f;
    [SerializeField] float currentFillValue = 0f;

    bool canDecrease = false;

    private void Update()
    {
        if(canDecrease)
        {
            if(circleSprite.fillAmount > 0)
            {
                currentFillValue -= fillRate;
                UpdateCircleSpriteFillAmounth();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            currentFillValue += fillRate;
            UpdateCircleSpriteFillAmounth();

            if(currentFillValue == maxFillValue)
            {
                BagContent bag = GetComponent<BagContent>();
                NPCRandomizer npcType = GetComponent<NPCRandomizer>();

                other.gameObject.GetComponent<PlayerRobbingManager>().StartRobbing(bag, npcType.GetNPCTypeIndex(), npcType.GetNPCColor());
            }
        }

        canDecrease = false;
    }

    private void OnTriggerExit(Collider other)
    {
        canDecrease = true;
    }

    public void UpdateCircleSpriteFillAmounth()
    {
        circleSprite.fillAmount = currentFillValue / maxFillValue;
    }
}
