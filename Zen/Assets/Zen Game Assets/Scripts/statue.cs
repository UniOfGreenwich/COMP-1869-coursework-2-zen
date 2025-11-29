using UnityEngine;

public class statue : MonoBehaviour
{
    public bool haveBeenSelected;
    public Vector3 sittingSpot;
    public bool isMissingStatue;
    public AudioSource AudioSource;
    [SerializeField] AudioClip pickUp;
    [SerializeField] AudioClip putDown;
    bool missing;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        missing = GameObject.FindAnyObjectByType<PlayerIntoraction>().inMissing;
        Pickup();
        PutDown();
        
       
    }

    void Pickup()
    {
        if (haveBeenSelected == true)
        {
            AudioSource.enabled = true;
            AudioSource.PlayOneShot(pickUp);
            
        }
    }

    void PutDown()
    {
        if (missing == false)
        {
            AudioSource.PlayOneShot(putDown);
        }
    }


}
