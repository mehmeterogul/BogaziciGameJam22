using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerRobbingManager : MonoBehaviour
{
    [SerializeField] GameObject playerCanvas;
    [SerializeField] GameObject bag;

    [SerializeField] ObjectRandomizer objectRandomizer;

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

    public void StartRobbing(int npcTypeIndex, int npcColorIndex)
    {
        this.npcColorIndex = npcColorIndex;
        this.npcTypeIndex = npcTypeIndex;

        StartBagPasswordStateAnimation();
    }

    public void FirstPasswordStagePassed(int value)
    {
        choosedColorIndex = value;
        stage1Image.sprite = colorSprites[value];
        PasswordSwitchAnimation();
    }

    public void SecondPasswordStagePassed(int value)
    {
        choosedTypeIndex = value;
        stage2Image.sprite = shapeSprites[value];
        CheckPasswordCorrect();
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
}
