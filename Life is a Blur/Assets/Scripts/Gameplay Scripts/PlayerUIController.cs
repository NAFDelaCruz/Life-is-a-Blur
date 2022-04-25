using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIController : MonoBehaviour
{
    public GameObject InteractNotif;
    public GameObject InspectNotif;
    public PlayerInteraction PlayerInteractionScript;

    // Update is called once per frame
    void Update()
    {
        if (PlayerInteractionScript.ObjectBehavior)
        {
            if (PlayerInteractionScript.ObjectBehavior.isInteractable)
                InteractNotif.SetActive(true);
            if (PlayerInteractionScript.ObjectBehavior.isInspectable)
                InspectNotif.SetActive(true);
        }

        if (!PlayerInteractionScript.ObjectBehavior)
        {
            InteractNotif.SetActive(false);
            InspectNotif.SetActive(false);
        }
    }
}
