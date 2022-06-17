using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class InspectToAllowCutscene : InteractableObject
{
    public PlayableDirector CutScene;

    public override InteractableObject Interact()
    {
        if (Input.GetMouseButtonDown(0)) CutScene.Play();

        return this;
    }
}
