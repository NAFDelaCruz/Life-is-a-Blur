using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTest : InteractableObject
{
    public override InteractableObject Interact()
    {
        if (Input.GetMouseButtonDown(0))
            Destroy(gameObject);

        return this;
    }
}
