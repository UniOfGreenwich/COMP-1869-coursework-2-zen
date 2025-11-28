using UnityEngine;

public class footsteps : MonoBehaviour
{
    public GameObject player;
    public AudioSource footstep;
    public Vector3 playervelocity;
    public GameObject triggerPoint;
    public bool OnWood;
    [SerializeField] AudioClip ground1;
    [SerializeField] AudioClip ground2;

    void Update()
    {
        playervelocity = GameObject.FindAnyObjectByType<playerMovment>().velocityInput;
        OnWood = GameObject.FindAnyObjectByType<triggerpoint>().onWood;
        if ((playervelocity != Vector3.zero) && (OnWood = false))
        {
            //footstep.enabled = true;
            footstep.clip = ground1;
        }
        else if ((playervelocity != Vector3.zero) && (OnWood = true))
        {
            //footstep.enabled = true;
            footstep.clip = ground2;
        }
        



    }


}

