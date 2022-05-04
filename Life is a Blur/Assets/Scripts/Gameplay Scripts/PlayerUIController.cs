using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    public GameObject InteractNotif;
    public GameObject InspectNotif;
    public Image Cursor;
    public PlayerInteraction PlayerInteractionScript;

    void Update()
    {
        if (PlayerInteractionScript.ObjectBehavior)
        {
            Cursor.color = new Color(255f, 255f, 255f, 1f);

            if (PlayerInteractionScript.ObjectBehavior.isInteractable)
                InteractNotif.SetActive(true);
            if (PlayerInteractionScript.ObjectBehavior.isInspectable)
                InspectNotif.SetActive(true);
        }

        if (!PlayerInteractionScript.ObjectBehavior)
        {
            Cursor.color = new Color(255f, 255f, 255f, 0.15f);

            InteractNotif.SetActive(false);
            InspectNotif.SetActive(false);
        }
    }
}
