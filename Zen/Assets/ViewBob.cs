using UnityEngine;

public class ViewBob : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Vector3 baseCameraLocalPos,inputForce;

    void Start()
    {
        baseCameraLocalPos = Camera.main.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        inputForce = GameObject.FindAnyObjectByType<playerMovment>().velocityInput;
        Vector3 bob = GetViewBobOffset(inputForce);
        Camera.main.transform.localPosition = baseCameraLocalPos + bob;
    }

    private Vector3 GetViewBobOffset(Vector3 Input, float bobFrequency = 8f, float bobAmplitude = 0.08f)
    {

        if (Input == Vector3.zero)
        {
            return Vector3.Lerp(Camera.main.transform.localPosition - baseCameraLocalPos, Vector3.zero, Time.deltaTime * 8f);
        }


        float t = Time.time * bobFrequency;


        float y = Mathf.Sin(t) * bobAmplitude;


        float x = Mathf.Cos(t * 0.5f) * bobAmplitude * 0.5f;

        return new Vector3(x, y, 0f);
    }
}
