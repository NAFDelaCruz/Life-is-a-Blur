using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerBlink : MonoBehaviour
{
    public PostProcessVolume volume;
    public DepthOfField dof;

    public GameObject blink01;
    public GameObject blink02;

    [Range(0.0f, 1.0f)]
    public float lerpValue;
    public float lerpSpeed;

    Vector3 BasePosition1;
    Vector3 TargetPosition1;

    Vector3 BasePosition2;
    Vector3 TargetPosition2;
    
    public float baseBlur;
    public float blinkBlur;

    // Start is called before the first frame update
    void Start()
    {
        BasePosition1 = blink01.transform.position;
        BasePosition2 = blink02.transform.position;

        TargetPosition1 = blink01.transform.position - new Vector3(0, 654, 0);
        TargetPosition2 = blink02.transform.position + new Vector3(0, 649, 0);

        volume.profile.TryGetSettings(out dof);
    }

    public void Squint(float ObjectDistance)
    {
        lerpValue = Mathf.Clamp(lerpValue += lerpSpeed, 0, 1);
        if (dof.focusDistance.value < ObjectDistance / 2f)
            dof.focusDistance.value = Mathf.Clamp(dof.focusDistance.value += (2f * Time.deltaTime), 0.3f, ObjectDistance / 2f);
        else if (dof.focusDistance.value > ObjectDistance / 2f)
            dof.focusDistance.value = Mathf.Clamp(dof.focusDistance.value -= (2f * Time.deltaTime), 0.3f, ObjectDistance / 2f);
        SetEyelids();
    }

    public void Unsquint()
    {
        lerpValue = Mathf.Clamp(lerpValue -= lerpSpeed, 0, 1);
        dof.focusDistance.value = Mathf.Clamp(dof.focusDistance.value -= (3f * Time.deltaTime), 0.3f, 2f);
        SetEyelids();
    }

    void SetEyelids()
    {
        blink01.transform.position = Vector3.Lerp(BasePosition1, TargetPosition1, lerpValue);
        blink02.transform.position = Vector3.Lerp(BasePosition2, TargetPosition2, lerpValue);
    }
}
