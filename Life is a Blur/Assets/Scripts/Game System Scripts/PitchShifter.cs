using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchShifter : MonoBehaviour
{

    AudioSource audioSource;

    public float minTime;
    public float maxTime;
    public float currentTime = 5f;

    public float defaultPitch;
    public float offsetPitch;

    

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        defaultPitch = audioSource.pitch;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime >= 0)
        {
            currentTime -= 1f * Time.deltaTime;
        }
        else
        {
            NewCycle();
        }
    }

    void NewCycle()
    {
        currentTime = Random.Range(minTime, maxTime);
        audioSource.pitch = defaultPitch + Random.Range(-offsetPitch, offsetPitch);
    }
}
