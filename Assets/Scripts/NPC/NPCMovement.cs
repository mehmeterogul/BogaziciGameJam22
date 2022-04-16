using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    public void StopWalk()
    {
        moveSpeed = 0;
    }

    public void ResumeWalk()
    {
        moveSpeed = 1f;
    }
}
