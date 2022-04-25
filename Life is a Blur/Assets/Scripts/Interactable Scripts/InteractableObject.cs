using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public bool isInteractable;
    public bool isInspectable;
    public List<string> InspectDialogue;

    public abstract InteractableObject Interact();
}
