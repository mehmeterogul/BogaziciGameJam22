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

    bool canDestroy = false;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        Invoke("SetCanDestroyTrue", 2f);
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

        if(canDestroy && other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            currentFillValue += fillRate;
            UpdateCircleSpriteFillAmounth();

            if(currentFillValue == maxFillValue && gameManager.gameState == GameManager.STATE.WALKING)
            {
                NPCRandomizer npcType = GetComponent<NPCRandomizer>();

                gameManager.ChangeGameStateToRobbing();
                other.gameObject.GetComponent<PlayerRobbingManager>().StartRobbing(npcType.GetNPCTypeIndex(), npcType.GetNPCColor());
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
