using UnityEngine;

public class footsteps : MonoBehaviour
{
    public GameObject player;
    public AudioSource footstep;
    public Vector3 playervelocity;
    public GameObject triggerPoint;
    public bool OnWood;
    bool OnWoodcheck;
    public triggerpoint Tp;
    [SerializeField] AudioClip ground1;
    [SerializeField] AudioClip ground2;

    void Update()
    {
        playervelocity = GameObject.FindAnyObjectByType<playerMovment>().velocityInput;
        
        if (Tp.onWood == true)
        {
            OnWoodcheck = true;
        }
        else if (Tp.onWood == false)
        {
            OnWoodcheck = false;
        }
        if ((playervelocity != Vector3.zero) && (OnWoodcheck == false))
        {
            footstep.enabled = true;
            footstep.clip = ground1;
        }
        else if ((playervelocity == Vector3.zero) && (OnWoodcheck == false))
        {
            footstep.enabled = false;
            footstep.clip = ground1;
        }
        
         if ((playervelocity != Vector3.zero) && (OnWoodcheck == true))
        {
            footstep.enabled = true;
            footstep.clip = ground2;
        }
        if ((playervelocity == Vector3.zero) && (OnWoodcheck == true))
        {
            footstep.enabled = false;
            footstep.clip = ground2;
        }

        Debug.Log(OnWoodcheck);





    }


}

