using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : Quest
{
    public SceneLoader SceneLoaderScript;
    bool isChanging = false;

    public override Quest QuestActions()
    {
        if (!isChanging)
        {
            isChanging = true;
            SceneLoaderScript.ChangeScene();
        }

        return this;
    }
}
