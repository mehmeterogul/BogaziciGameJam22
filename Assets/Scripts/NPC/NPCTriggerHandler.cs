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
    bool canTrigger = true;

    GameManager gameManager;

    bool canDestroy = false;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        Invoke("SetCanDestroyTrue", 5f);
    }

    void Update()
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

    void SetCanDestroyTrue()
    {
        canDestroy = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canDecrease = false;
        }

        if (canDestroy && other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject, 5f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && canTrigger)
        {
            currentFillValue += fillRate;
            UpdateCircleSpriteFillAmounth();

            if(currentFillValue == maxFillValue && gameManager.gameState == GameManager.STATE.WALKING)
            {
                NPCRandomizer npcType = GetComponent<NPCRandomizer>();

                gameManager.ChangeGameStateToRobbing();
                other.gameObject.GetComponent<PlayerRobbingManager>().StartRobbing(npcType.GetNPCTypeIndex(), npcType.GetNPCColor());

                canTrigger = false;
                currentFillValue = 0;
                UpdateCircleSpriteFillAmounth();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canDecrease = true;
        }
    }

    public void UpdateCircleSpriteFillAmounth()
    {
        circleSprite.fillAmount = currentFillValue / maxFillValue;
    }
}
