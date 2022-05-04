using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Quest CurrentQuest;
    Outline QuestOutline;

    void Update()
    {
        CurrentQuest?.QuestActions();
    }
}
