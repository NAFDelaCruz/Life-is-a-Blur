using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Quest : MonoBehaviour
{
    public GameObject QuestObject;
    public List<string> QuestDialogue;

    public abstract Quest QuestActions();
}
