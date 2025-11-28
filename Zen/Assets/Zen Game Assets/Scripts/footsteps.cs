using UnityEngine;

public class footsteps : MonoBehaviour
{
    public AudioSource footstep;

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            footstep.enabled = true;
        }
        else
        {
            footstep.enabled = false;
        }
    }
}

