using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 lastMousePosition;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed;

    public Animator animator;

    Vector3 delta;
    Vector3 temp;

    // Start is called before the first frame update
    void Start()
    {

    }

    void FixedUpdate()
    {
        if (temp.x > 0.05f || temp.x < -0.05f || temp.z > 0.05f || temp.z < -0.05f)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                Quaternion.LookRotation(temp, Vector3.up),
                rotationSpeed * Time.deltaTime
                );
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
        }

        if(Input.GetMouseButton(0))
        {
            delta = Input.mousePosition - lastMousePosition;

            temp = delta.normalized;
            temp.z = temp.y;
            temp.y = 0;

            if (temp.x > 0.05f || temp.x < -0.05f || temp.z > 0.05f || temp.z < -0.05f)
            {
                animator.SetBool("isWalking", true);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            delta = Vector3.zero;
            temp = Vector3.zero;

            animator.SetBool("isWalking", false);
        }
    }
}
