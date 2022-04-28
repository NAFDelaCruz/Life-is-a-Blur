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

    public bool isBlinking = false;

    [Range(0.0f, 1.0f)]
    public float lerpValue;
    public float lerpSpeed;

    Vector3 BasePosition1;
    Vector3 TargetPosition1;

    Vector3 BasePosition2;
    Vector3 TargetPosition2;

    public float currentBlur;
    public float baseBlur;
    public float blinkBlur;

    // Start is called before the first frame update
    void Start()
    {
        BasePosition1 = blink01.transform.position;
        BasePosition2 = blink02.transform.position;

        TargetPosition1 = blink01.transform.position - new Vector3(0, 282, 0);
        TargetPosition2 = blink02.transform.position + new Vector3(0, 282, 0);

        volume.profile.TryGetSettings(out dof);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.B))
        {
            if (lerpValue <= 1)
            {
                lerpValue += lerpSpeed;
            }
            else
            {
                lerpValue = 1;
            }
            
        }
        else
        {
            if (lerpValue >= 0)
            {
                lerpValue -= lerpSpeed;
            }
            else
            {
                lerpValue = 0;
            }
        }

        blink01.transform.position = Vector3.Lerp(BasePosition1, TargetPosition1, lerpValue);
        blink02.transform.position = Vector3.Lerp(BasePosition2, TargetPosition2, lerpValue);

        currentBlur = Mathf.Lerp(baseBlur, blinkBlur, lerpValue);
        dof.aperture.value = currentBlur;
    }
}
