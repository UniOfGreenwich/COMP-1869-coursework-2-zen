using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Audio_MicDetector : MonoBehaviour
{
    AudioClip mic;
    public string device;
    int window = 256;
    public float WaitTimer, averageVol, framecount, vol, breethInTime;
    public Slider breathSlider;
    public Image fill;
    public bool breethingIn;
    public GameObject BreatheIN, BreatheOUT;


    void Start()
    {
        device = Microphone.devices[0];
        mic = Microphone.Start(device, true, 1, 44100);
    }
    void Update()
    {
        if (averageVol <= 0 && WaitTimer <= 0)
        {
            fill.color = Color.green;
        }
        else if (WaitTimer <= 0) { fill.color = Color.red; }

    }

    private void FixedUpdate()
    {
        if (breethingIn)
        {
            BreatheIN.SetActive(true);
            BreatheOUT.SetActive(false);
            breathSlider.value += breethInTime;
            if (breathSlider.value >= 100) { breethingIn = false; }

        }
        else
        {
            BreatheIN.SetActive(false);
            BreatheOUT.SetActive(true);
            vol = GetVolume();
            if (vol < 0.01) { vol = 0; }
            if (vol > 2) { vol = 2; }
            averageVol = averageVol - vol;
            breathSlider.value = averageVol;

            WaitTimer -= Time.deltaTime;

            if (WaitTimer < -5f)
            {
                BreatheOUT.SetActive(false);
                BreatheIN.SetActive(false);
                gameObject.SetActive(false);
            }
        }
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

    private void OnDisable()
    {
        averageVol = 100f;
        WaitTimer = 4;
        fill.color = Color.white;
    }
}
