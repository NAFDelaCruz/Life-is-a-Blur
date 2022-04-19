using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Set Components")]
    public GameObject PlayerBody;
    public GameObject PlayerHead;
    public float MaxHeadElevationDegree;
    public float MinHeadDepressionDegree;
    float yRotation;
    float xRotation;

    [Header("Player Stats")]
    public float PlayerMoveSpeed;
    public float PlayerLookSensitivity;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float XBodyMovement = Input.GetAxis("Horizontal");
        float YBodyMovement = Input.GetAxis("Vertical");
        float XHeadMovement = Input.GetAxis("Mouse X");
        float YHeadMovement = Input.GetAxis("Mouse Y");

        PlayerBody.transform.Translate(Vector3.forward * YBodyMovement * PlayerMoveSpeed * Time.deltaTime);
        PlayerBody.transform.Translate(Vector3.right * XBodyMovement * PlayerMoveSpeed * Time.deltaTime);
        PlayerBody.transform.Rotate(Vector3.up * XHeadMovement * PlayerLookSensitivity);

        yRotation -= YHeadMovement;
        yRotation = Mathf.Clamp(yRotation, -MaxHeadElevationDegree, MinHeadDepressionDegree);
        xRotation += XHeadMovement;


        Quaternion rotation = PlayerHead.transform.rotation;
        rotation.eulerAngles = new Vector3(yRotation, xRotation, 0);
        PlayerHead.transform.rotation = rotation;
    }
}
