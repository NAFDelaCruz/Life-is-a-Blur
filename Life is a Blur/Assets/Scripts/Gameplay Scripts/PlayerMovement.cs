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
    public GameObject LookAt; //added
    public float MaxHeadElevationDegree;
    public float MinHeadDepressionDegree;
    [HideInInspector]
    public float yRotation;
    [HideInInspector]
    public float xRotation;
    [HideInInspector] //added
    Animator anim;
    //[HideInInspector] //added
    public float offset;

    Rigidbody PlayerRigidBody;
    Vector3 MovementInput;
    Quaternion Rotation;
    AudioSource Footsteps;
    [HideInInspector]
    public bool isGameNotPaused;

    public void Start()
    {
        Footsteps = GetComponent<AudioSource>();
        PlayerRigidBody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        isGameNotPaused = true;
    }

    void Update()
    {
        if (isGameNotPaused)
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

            //added
            LookAt.transform.position = transform.position + transform.up + (transform.forward * offset);
        }
        
        //Debug.Log(transform.forward);
        Move();
    }

    void Move()
    {
        Vector3 MoveVector = transform.TransformDirection(MovementInput) * MoveSpeed;
        PlayerRigidBody.velocity = new Vector3(MoveVector.x, PlayerRigidBody.velocity.y, MoveVector.z);

        //animation
        if(PlayerRigidBody.velocity.magnitude > -.2f || PlayerRigidBody.velocity.magnitude < .2f)
        {
            anim.SetTrigger("Walk");
        }
        else
        {
            anim.SetTrigger("Idle");
        }
        
    }
}
