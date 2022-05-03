using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Quest CurrentQuest;
    DialogueManager DialogueManagerScript;

    void Update()
    {
        CurrentQuest?.QuestActions();
    }
}
