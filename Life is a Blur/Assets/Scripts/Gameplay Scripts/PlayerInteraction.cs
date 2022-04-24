using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public DialogueManager DialogueManagerScript; 

    RaycastHit Hit;
    GameObject InteractableObject;
    InteractableObject ObjectBehavior;
    float DistanceToObject;

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out Hit))
        {
            InteractableObject = Hit.collider.gameObject;
        }

        DistanceToObject = Vector2.Distance(transform.position, InteractableObject.transform.position);
        if (DistanceToObject < 3.5f && !ObjectBehavior)
        {
            ObjectBehavior = InteractableObject.GetComponent<InteractableObject>();

        }
        else if (ObjectBehavior)
        {
            ObjectBehavior = null;
        }

        if (ObjectBehavior)
        {
            ObjectBehavior.Interact();

            if (Input.GetMouseButtonDown(1) && ObjectBehavior.isInspectable)
                DialogueManagerScript.Dialogues = ObjectBehavior.InspectDialogue;
        }
    }
}
