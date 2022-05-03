using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tutorial : MonoBehaviour
{
    public List<Sprite> TutorialPrompts;
    public GameObject TutorialUI;
    public int TutorialIndex = 0;

    public abstract Tutorial TutorialActions();
}
