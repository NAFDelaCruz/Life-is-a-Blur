using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectEyeglass : CollectQuest
{
    public GameObject PPVolume;

    private void Update()
    {
        TurnOff();
    }
    void TurnOff()
    {
        if (TargetGameObjects.Count == 0) PPVolume.SetActive(false);
    }
}
