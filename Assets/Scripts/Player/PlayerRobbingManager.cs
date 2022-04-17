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
    [SerializeField] Sprite defaultImage;
    [SerializeField] Sprite[] colorSprites;
    [SerializeField] Sprite[] shapeSprites;
    [SerializeField] GameObject backButton;

    int npcTypeIndex = 0;
    int npcColorIndex = 0;

    int choosedColorIndex = 0;
    int choosedTypeIndex = 0;

    Camera mainCamera;

    List<Item> collectedItems = new List<Item>();
    [SerializeField] Inventory inventory;

    [Header("Stress Bar")]
    [SerializeField] Image stressBarSprite;
    [SerializeField] Color stressBarDefaultColor;
    [SerializeField] Color stressBarWarningColor;
    [SerializeField] float emptyRate = 0.1f;
    [SerializeField] float maxFillValue = 300f;
    [SerializeField] float currentFillValue = 0f;

    bool canDecrease = false;

    AudioSource audioSource;
    [SerializeField] AudioClip clickSound;
    [SerializeField] AudioClip pickSound;
    [SerializeField] AudioClip backButtonSound;
    [SerializeField] AudioClip panelOpenSound;
    [SerializeField] AudioClip failSound;

    void Start()
    {
        mainCamera = Camera.main;
        audioSource = GetComponent<AudioSource>();
        currentFillValue = maxFillValue;
        inventory = GetComponent<Inventory>();
        backButton.SetActive(false);
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

        if(currentFillValue <= maxFillValue * 0.25f)
        {
            backButton.GetComponent<Animator>().SetBool("isHurry", true);
            stressBarSprite.color = stressBarWarningColor;
        }

        if(currentFillValue < 0.1f)
        {
            RobbingFailed();
        }
    }

    public void StartRobbing(int npcTypeIndex, int npcColorIndex)
    {
        this.npcColorIndex = npcColorIndex;
        this.npcTypeIndex = npcTypeIndex;

        backButton.GetComponent<Animator>().SetBool("isHurry", false);
        stressBarSprite.color = stressBarDefaultColor;
        
        currentFillValue = maxFillValue;
        UpdateStressBarFillAmounth();

        audioSource.PlayOneShot(panelOpenSound, 1f);

        StartBagPasswordStateAnimation();
        Invoke("CanDecrease", 1f);
    }

    void RobbingFailed()
    {
        audioSource.PlayOneShot(failSound, 1f);

        canDecrease = false;
        backButton.SetActive(false);
        collectedItems.Clear();
        RobbingStoppedAnimation();
        FindObjectOfType<GameManager>().ChangeGameStateToWalking();
    }

    void RobbingSuccess()
    {
        canDecrease = false;
        inventory.AddItem(collectedItems);
        collectedItems.Clear();
        RobbingStoppedAnimation();
        FindObjectOfType<GameManager>().ChangeGameStateToWalking();
    }

    void CanDecrease()
    {
        canDecrease = true;
    }

    public void FirstPasswordStagePassed(int value)
    {
        audioSource.PlayOneShot(clickSound, 1f);

        if(value == npcColorIndex)
        {
            choosedColorIndex = value;
            stage1Image.sprite = colorSprites[value];
            PasswordSwitchAnimation();
        }
        else
        {
            DecreaseStressBarValue(20f);
        }
    }

    private void DecreaseStressBarValue(float value)
    {
        currentFillValue -= value;
        UpdateStressBarFillAmounth();
    }

    public void SecondPasswordStagePassed(int value)
    {
        audioSource.PlayOneShot(clickSound, 1f);

        if (value == npcTypeIndex)
        {
            choosedTypeIndex = value;
            stage2Image.sprite = shapeSprites[value];
            CheckPasswordCorrect();
        }
        else
        {
            DecreaseStressBarValue(20f);
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
    
    void PanelCloseAnimation()
    {
        playerCanvas.transform.DOLocalMove(new Vector3(0, 9.13f, -8.72f), 1f);
    }

    void BagAppearAnimation()
    {
        bag.transform.DOLocalMove(new Vector3(0, 0.075f, 0.84f), 0.5f);
    }

    void BagResetAnimation()
    {
        bag.transform.DOLocalMove(new Vector3(0, 1f, 0.84f), 1f);
    }

    void ResetBagPosition()
    {
        bag.transform.position = new Vector3(0, 0.58f, 0.84f);
    }

    void PasswordSwitchAnimation()
    {
        passwordPick.DOLocalMoveX(-540, 0.5f);
    }

    void PasswordSwitchPanelReset()
    {
        passwordPick.DOLocalMoveX(0, 0.5f);
    }

    void CheckPasswordCorrect()
    {
        if(choosedColorIndex == npcColorIndex && choosedTypeIndex == npcTypeIndex)
        {
            OpenSuitcase();
            RandomizeObjects();
            Invoke("ActivateBackButton", 0.5f);
        }
    }

    void ActivateBackButton()
    {
        backButton.SetActive(true);
    }

    void OpenSuitcase()
    {
        bagCover.DORotate(new Vector3(-200, 0, 0), 0.5f);
    }

    void CloseSuitcase()
    {
        bagCover.localRotation = new Quaternion(0, 0, 0, 0);
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
                audioSource.PlayOneShot(pickSound, 1f);

                AddItemToList(selection.GetComponent<Item>());
                selection.gameObject.SetActive(false);
                DecreaseStressBarValue(55f);
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

    void RobbingStoppedAnimation()
    {
        ResetAnimations();
        currentFillValue = maxFillValue;
    }

    public void BackButton()
    {
        audioSource.PlayOneShot(backButtonSound, 1f);

        RobbingSuccess();
        backButton.SetActive(false);
    }

    void ResetAnimations()
    {
        PanelCloseAnimation();
        BagResetAnimation();
        PasswordSwitchPanelReset();
        Invoke("CloseSuitcase", 2f);
        stage1Image.sprite = defaultImage;
        stage2Image.sprite = defaultImage;
    }
}
