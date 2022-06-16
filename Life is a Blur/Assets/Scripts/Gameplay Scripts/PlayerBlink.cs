using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerBlink : MonoBehaviour
{
    public PostProcessVolume volume;
    public DepthOfField dof;
    public Animator SquintAnimator;
    
    public float baseBlur;
    public float blinkBlur;

    public float SquintTimeLimit;
    public float SquintResetTime;
    float TimeHeld;
    bool isButtonHeld;
    bool isSquintAllowed = true;

    // Start is called before the first frame update
    void Start()
    {
        volume.profile.TryGetSettings(out dof);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) TimeHeld = Time.time;

        if ((TimeHeld + SquintTimeLimit) < Time.time && isButtonHeld) StartCoroutine(SquintBan());

        if (Input.GetKey(KeyCode.B) && isSquintAllowed)
        {
            SquintAnimator.SetBool("IsSquinting", true);
            isButtonHeld = true;
            Squint();
        }
        else
        {
            SquintAnimator.SetBool("IsSquinting", false);
            isButtonHeld = false;
            Unsquint();
        }
    }

    IEnumerator SquintBan()
    {
        isSquintAllowed = false;
        SquintAnimator.SetBool("IsSquintingBanned", true);
        yield return new WaitForSeconds(SquintResetTime);
        isSquintAllowed = true;
        SquintAnimator.SetBool("IsSquintingBanned", false);
    }

    public void Squint()
    {
        dof.focusDistance.value = Mathf.Clamp(dof.focusDistance.value += (2f * Time.deltaTime), 0.3f, 1f);
    }

    public void Unsquint()
    {
        dof.focusDistance.value = Mathf.Clamp(dof.focusDistance.value -= (2f * Time.deltaTime), 0.3f, 1f);
    }
}
