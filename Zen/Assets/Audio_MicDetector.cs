using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Audio_MicDetector : MonoBehaviour
{
    AudioClip mic;
    string device;
    int window = 256;
    public float averageWaitTimer, averageVol,framecount, vol;
    public GameObject breathSlider;


    void Start()
    {
        device = Microphone.devices[0];
        mic = Microphone.Start(device, true, 1, 44100);
    }
    void Update()
    {
         vol = GetVolume();
        averageVol = averageVol + vol;
        breathSlider.GetComponent<Slider>().value = averageWaitTimer;
    }

    private void FixedUpdate()
    {
        if (averageWaitTimer <= 0) 
        {
            averageWaitTimer = 3;
            framecount = 0;
            vol = 0;
        }
        else { averageVol = averageVol + vol; framecount++; }

        averageWaitTimer -= Time.deltaTime;
        
    }

    float GetVolume()
    {
        float[] data = new float[window];
        int pos = Microphone.GetPosition(device);

        if (pos - window < 0)
            return 0f;

        mic.GetData(data, pos - window);

        float sum = 0f;
        for (int i = 0; i < window; i++)
            sum += data[i] * data[i];

        return Mathf.Sqrt(sum / window);
    }
}
