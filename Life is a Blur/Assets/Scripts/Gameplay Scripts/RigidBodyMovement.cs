using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyMovement : MonoBehaviour
{
    Vector3 movementInput;

    public float moveSpeed;

    Rigidbody rb;

    public void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        movementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        Move();
    }

    void Move()
    {
        Vector3 MoveVector = transform.TransformDirection(movementInput) * moveSpeed;
        rb.velocity = new Vector3(MoveVector.x, rb.velocity.y, MoveVector.z);

    }
}
