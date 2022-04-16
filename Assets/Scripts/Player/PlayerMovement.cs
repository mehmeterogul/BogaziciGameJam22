using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;

    [Header("Speed Values")]
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed;

    [Header("Movement Vector Values")]
    Vector3 lastMousePosition;
    Vector3 delta;
    Vector3 normalizedDelta;

    // Start is called before the first frame update
    void Start()
    {

    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        // if normalized delta axis values bigger than 0.05f, then move
        if (normalizedDelta.x > 0.05f || normalizedDelta.x < -0.05f || normalizedDelta.z > 0.05f || normalizedDelta.z < -0.05f)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                Quaternion.LookRotation(normalizedDelta, Vector3.up),
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
            SetDeltaVariables();

            if (normalizedDelta.x > 0.05f || normalizedDelta.x < -0.05f || normalizedDelta.z > 0.05f || normalizedDelta.z < -0.05f)
            {
                animator.SetBool("isWalking", true);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            delta = Vector3.zero;
            normalizedDelta = Vector3.zero;

            animator.SetBool("isWalking", false);
        }
    }

    void SetDeltaVariables()
    {
        delta = Input.mousePosition - lastMousePosition;

        normalizedDelta = delta.normalized;
        normalizedDelta.z = normalizedDelta.y;
        normalizedDelta.y = 0;
    }
}
