using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRandomizer : MonoBehaviour
{
    /*
     * 0- Circle
     * 1- Triangle
     * 2- Stick
     */
    [SerializeField] GameObject[] npcObjects;
    [SerializeField] Material[] npcMaterials;
    int choosedObjectIndex;
    int choosedColorIndex;

    // Start is called before the first frame update
    void Start()
    {
        // activate random npc objects
        choosedObjectIndex = Random.Range(0, npcObjects.Length);
        npcObjects[choosedObjectIndex].SetActive(true);

        // set random material to the choosed npc object
        choosedColorIndex = Random.Range(0, npcMaterials.Length);
        npcObjects[choosedObjectIndex].GetComponent<MaterialAssigner>().AssignMaterial(npcMaterials[choosedColorIndex]);
    }
}
