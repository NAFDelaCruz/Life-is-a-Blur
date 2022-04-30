using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public DialogueManager DialogueManagerScript;
    public PlayerBlink PlayerBlinkScript;
    public float InteractionDistance;

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

        if (Input.GetKey(KeyCode.B))
        {
            if (DistanceToObject > 3f)
                PlayerBlinkScript.Squint(3f);
            else
                PlayerBlinkScript.Squint(DistanceToObject);
        }
        else
        {
            PlayerBlinkScript.Unsquint();
        }

        if (DistanceToObject <= InteractionDistance && !ObjectBehavior)
        {
            ObjectBehavior = InteractableObject.GetComponent<InteractableObject>();
            InteractableObject.AddComponent<Outline>();
        }
        else if (DistanceToObject > InteractionDistance && ObjectBehavior)
        {
            Destroy(InteractableObject.GetComponent<Outline>());
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