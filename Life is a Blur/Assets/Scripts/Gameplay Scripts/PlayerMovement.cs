using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Player Stats")]
    public float MoveSpeed;
    public float PlayerLookSensitivity;

    [Header("Set Components")]
    public GameObject PlayerHead;
    public float MaxHeadElevationDegree;
    public float MinHeadDepressionDegree;
    [HideInInspector]
    public float yRotation;
    [HideInInspector]
    public float xRotation;

    Rigidbody PlayerRigidBody;
    Vector3 MovementInput;
    Quaternion Rotation;

    public void Start()
    {
        PlayerRigidBody = GetComponent<Rigidbody>();  
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        MovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        float XHeadMovement = Input.GetAxis("Mouse X");
        float YHeadMovement = Input.GetAxis("Mouse Y");
        transform.Rotate(Vector3.up * XHeadMovement * PlayerLookSensitivity);

        yRotation -= YHeadMovement;
        yRotation = Mathf.Clamp(yRotation, -MaxHeadElevationDegree, MinHeadDepressionDegree);
        xRotation += XHeadMovement;
        Rotation = PlayerHead.transform.rotation;
        Rotation.eulerAngles = new Vector3(yRotation, transform.eulerAngles.y, 0);
        PlayerHead.transform.rotation = Rotation;

        Move();
    }

    void Move()
    {
        Vector3 MoveVector = transform.TransformDirection(MovementInput) * MoveSpeed;
        PlayerRigidBody.velocity = new Vector3(MoveVector.x, PlayerRigidBody.velocity.y, MoveVector.z);
    }
}
