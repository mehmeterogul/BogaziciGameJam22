using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 lastMousePosition;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
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
            Vector3 delta = Input.mousePosition - lastMousePosition;

            Vector3 temp = delta.normalized;
            temp.z = temp.y;
            temp.y = 0;

            if (delta.x > 0.05f || delta.x < -0.05f || delta.y > 0.05f || delta.y < -0.05f)
            {
                transform.Translate(Vector3.forward *  moveSpeed * Time.deltaTime);
                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation,
                    Quaternion.LookRotation(temp, Vector3.up),
                    rotationSpeed * Time.deltaTime
                    );
            }
        }
    }
}
