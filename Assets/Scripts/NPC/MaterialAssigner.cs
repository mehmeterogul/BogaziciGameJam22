using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialAssigner : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer npcMeshRenderer;
    
    public void AssignMaterial(Material material)
    {
        npcMeshRenderer.material = material;
    }
}
