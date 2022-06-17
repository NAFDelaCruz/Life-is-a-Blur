using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioHandler : MonoBehaviour
{
    AudioSource Footsteps;
    Rigidbody PlayerRigidBody;
    PlayerMovement PlayerMovementScript;
    bool Contraints;

    void Start()
    {
        Footsteps = GetComponent<AudioSource>();
        PlayerRigidBody = GetComponent<Rigidbody>();
        PlayerMovementScript = GetComponent<PlayerMovement>();
        PlayerRigidBody.constraints = ~RigidbodyConstraints.FreezePosition;
    }
    
    void Update()
    {
        if ((Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical")) && !Footsteps.isPlaying && PlayerMovementScript.enabled && PlayerRigidBody.constraints == ~RigidbodyConstraints.FreezePosition) Footsteps.Play();
        else if ((!Input.GetButton("Horizontal") && !Input.GetButton("Vertical") && Footsteps.isPlaying) || !PlayerMovementScript.enabled) Footsteps.Stop();
    }
}
