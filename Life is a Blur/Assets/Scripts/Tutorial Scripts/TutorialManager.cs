using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public Tutorial CurrentTutorial;

    void Update()
    {
        CurrentTutorial?.TutorialActions();
    }
}
