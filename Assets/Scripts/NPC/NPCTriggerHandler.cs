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
    GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if(canDecrease)
        {
            if(circleSprite.fillAmount > 0)
            {
                currentFillValue -= (fillRate * 3);
                UpdateCircleSpriteFillAmounth();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        canDecrease = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            currentFillValue += fillRate;
            UpdateCircleSpriteFillAmounth();

            if(currentFillValue == maxFillValue && gameManager.gameState == GameManager.STATE.WALKING)
            {
                BagContent bag = GetComponent<BagContent>();
                NPCRandomizer npcType = GetComponent<NPCRandomizer>();

                gameManager.ChangeGameStateToRobbing();
                other.gameObject.GetComponent<PlayerRobbingManager>().StartRobbing(bag, npcType.GetNPCTypeIndex(), npcType.GetNPCColor());
            }
        }
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
