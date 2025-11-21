using Unity.VisualScripting;
using UnityEngine;


public class cameraViewbob : MonoBehaviour
{
    public bool isWalking;

    public GameObject player;

    [Range(0, 10)]
    public float bobIntevcity;

    
    Vector3 camPoz;

    public float hight, normHight;


    public float minHight;

    bool camUP = false;

    void Start()
    {
        normHight = player.transform.position.y +0.45f;
        minHight = normHight - minHight;
    }

    void FixedUpdate()
    {
        camPoz = gameObject.transform.position;

        if (isWalking)
        {
            if (camUP == false)
            {
                hight = hight - bobIntevcity;
                if(hight <= minHight) {camUP = true;}
            }
            else
            {
                hight = hight + bobIntevcity;
                if(hight >= normHight) {camUP=false;}
            }
        }
        gameObject.transform.position = new Vector3 (player.transform.position.x,  hight  ,player.transform.position.z);
    }
}
