using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayCutsceneInteraction : InteractableObject
{
    public PlayableDirector CutScene;

    public override InteractableObject Interact()
    {
        CutScene.Play();

        return this;
    }
}
