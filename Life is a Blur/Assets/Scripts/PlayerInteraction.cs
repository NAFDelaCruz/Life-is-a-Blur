using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject InteractNotif;

    RaycastHit Hit;
    GameObject InteractableObject;
    float DistanceToObject;
    bool IsObjectInRange = false;

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out Hit))
        {
            InteractableObject = Hit.collider.gameObject;
        }

        DistanceToObject = Vector3.Distance(transform.position, InteractableObject.transform.position);
        if (DistanceToObject < 3.5f)
        {
            InteractNotif.SetActive(true);
            IsObjectInRange = true;
        } else
        {
            InteractNotif.SetActive(false);
            IsObjectInRange = false;
        }

        if (Input.GetKeyDown(KeyCode.F) && IsObjectInRange)
        {
            Destroy(InteractableObject);
        }
    }
}
