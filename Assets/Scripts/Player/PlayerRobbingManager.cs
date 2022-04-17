using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerRobbingManager : MonoBehaviour
{

    [SerializeField] ObjectRandomizer objectRandomizer;

    [Header("Password Stage Components")]
    [SerializeField] GameObject playerCanvas;
    [SerializeField] GameObject bag;
    [SerializeField] Transform bagCover;
    [SerializeField] RectTransform passwordPick;
    [SerializeField] Image stage1Image;
    [SerializeField] Image stage2Image;
    [SerializeField] Sprite[] colorSprites;
    [SerializeField] Sprite[] shapeSprites;

    int npcTypeIndex = 0;
    int npcColorIndex = 0;

    int choosedColorIndex = 0;
    int choosedTypeIndex = 0;

    Camera mainCamera;

    List<Item> collectedItems = new List<Item>();

    [Header("Stress Bar")]
    [SerializeField] Image stressBarSprite;
    [SerializeField] float emptyRate = 0.1f;
    [SerializeField] float maxFillValue = 300f;
    [SerializeField] float currentFillValue = 0f;

    bool canDecrease = false;

    void Start()
    {
        mainCamera = Camera.main;
        currentFillValue = maxFillValue;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            SelectObjects();
        }

        if(canDecrease)
        {
            if (stressBarSprite.fillAmount > 0)
            {
                currentFillValue -= emptyRate;
                UpdateStressBarFillAmounth();
            }
        }
    }

    public void StartRobbing(int npcTypeIndex, int npcColorIndex)
    {
        this.npcColorIndex = npcColorIndex;
        this.npcTypeIndex = npcTypeIndex;

        StartBagPasswordStateAnimation();
        Invoke("CanDecrease", 1f);
    }

    void CanDecrease()
    {
        canDecrease = true;
    }

    public void FirstPasswordStagePassed(int value)
    {
        if(value == npcColorIndex)
        {
            choosedColorIndex = value;
            stage1Image.sprite = colorSprites[value];
            PasswordSwitchAnimation();
        }
        else
        {
            currentFillValue -= 10f;
            UpdateStressBarFillAmounth();
        }
    }

    public void SecondPasswordStagePassed(int value)
    {
        if(value == npcTypeIndex)
        {
            choosedTypeIndex = value;
            stage2Image.sprite = shapeSprites[value];
            CheckPasswordCorrect();
        }
        else
        {
            currentFillValue -= 10f;
            UpdateStressBarFillAmounth();
        }
    }

    void StartBagPasswordStateAnimation()
    {
        PanelAppearAnimation();
        Invoke("BagAppearAnimation", 0.5f);
    }

    void PanelAppearAnimation()
    {
        playerCanvas.transform.DOLocalMove(new Vector3(0, 10.2f, -6.88f), 1f);
    }
    
    void BagAppearAnimation()
    {
        bag.transform.DOLocalMove(new Vector3(0, 0.075f, 0.84f), 0.5f);
    }

    void PasswordSwitchAnimation()
    {
        passwordPick.DOLocalMoveX(-540, 0.5f);
    }

    void CheckPasswordCorrect()
    {
        if(choosedColorIndex == npcColorIndex && choosedTypeIndex == npcTypeIndex)
        {
            OpenSuitcase();
            RandomizeObjects();
        }
        else
        {
            Debug.Log("Yanlýþ");
        }
    }

    void OpenSuitcase()
    {
        bagCover.DORotate(new Vector3(-200, 0, 0), 0.5f);
    }

    void RandomizeObjects()
    {
        objectRandomizer.Randomize();
    }

    void SelectObjects()
    {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;

            if (selection.gameObject.CompareTag("Object"))
            {
                AddItemToList(selection.GetComponent<Item>());
                selection.gameObject.SetActive(false);
            }
        }
    }

    void AddItemToList(Item item)
    {
        collectedItems.Add(item);
    }

    public void UpdateStressBarFillAmounth()
    {
        stressBarSprite.fillAmount = currentFillValue / maxFillValue;
    }
}
