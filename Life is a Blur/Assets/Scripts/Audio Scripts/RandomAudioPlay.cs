using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudioPlay : MonoBehaviour
{
    public AudioSource thisAudio;

    public float timeToPlay;

    public float minTime;
    public float maxTime;
    // Start is called before the first frame update
    void Start()
    {
        thisAudio = this.GetComponent<AudioSource>();
        SetRandomTimer();
    }

    // Update is called once per frame
    void Update()
    {
        timeToPlay -= Time.deltaTime;
        if (timeToPlay <= 0)
        {
            thisAudio.Play();
            SetRandomTimer();
        }
    }

    void SetRandomTimer()
    { 
        timeToPlay = Random.Range(minTime, maxTime);
    }
}
