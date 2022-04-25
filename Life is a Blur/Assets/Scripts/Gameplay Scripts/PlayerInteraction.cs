using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public DialogueManager DialogueManagerScript; 

    RaycastHit Hit;
    GameObject InteractableObject;
    [HideInInspector]
    public InteractableObject ObjectBehavior;
    float DistanceToObject;

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out Hit))
        {
            InteractableObject = Hit.collider.gameObject;
        }

        DistanceToObject = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(Hit.point.x, Hit.point.z));
        if (DistanceToObject <= 1.25f && !ObjectBehavior)
        {
            ObjectBehavior = InteractableObject.GetComponent<InteractableObject>();

        }
        else if (DistanceToObject > 1.25f && ObjectBehavior)
        {
            ObjectBehavior = null;
        }

        if (ObjectBehavior)
        {
            if (ObjectBehavior.isInteractable)
                ObjectBehavior.Interact();

            if (Input.GetMouseButtonDown(1) && ObjectBehavior.isInspectable && DialogueManagerScript.isDialogueDone)
            {
                DialogueManagerScript.Dialogues = ObjectBehavior.InspectDialogue;
                DialogueManagerScript.StartDialogue();
            }
        }
    }
}
