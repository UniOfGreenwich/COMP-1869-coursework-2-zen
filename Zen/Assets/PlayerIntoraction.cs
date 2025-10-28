using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerIntoraction : MonoBehaviour
{
    public GameObject UI_Einteract, BreethBar, currentStachue;
    bool E_flashActive, sat;
    public float camSpeed, startTime;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (sat) LerpPositionToSitingSpot();

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "statue" && E_flashActive == false && !other.GetComponent<statue>().haveBeenSelected)
        {
            StartCoroutine(E_flash());
            currentStachue = other.gameObject;
        }

        if (Input.GetKey(KeyCode.E) && !other.GetComponent<statue>().haveBeenSelected) 
        { StartCoroutine(E_flashSelected()); 
            other.GetComponent<statue>().haveBeenSelected = true; }
    }

    private IEnumerator E_flash()
    {
        E_flashActive = true;
        UI_Einteract.SetActive(true);
        yield return new WaitForSeconds(1);
        UI_Einteract.SetActive(false);
        yield return new WaitForSeconds(1);
        E_flashActive = false;
    }
    private IEnumerator E_flashSelected()
    {
        UI_Einteract.SetActive(true) ;
        E_flashActive = false;
        UI_Einteract.GetComponent<Image>().color = Color.green;
        yield return new WaitForSeconds(1);
        UI_Einteract.SetActive(false) ;
        StartCoroutine(StatueSit());
    }

     private IEnumerator StatueSit()
    {
        GetComponent<playerMovment>().enabled = false;
        yield return new WaitForSeconds(1);
        startTime = Time.time;
        sat = true;
        GetComponent<CapsuleCollider>().enabled = false;  
        GetComponent<Rigidbody>().useGravity = false;  
        BreethBar.SetActive(true);
        yield return new WaitForSeconds(9);
        GetComponent<CapsuleCollider>().enabled = true;  
        GetComponent<Rigidbody>().useGravity = true;  
        sat = false;
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y + 0.45f,transform.position.z);
        BreethBar.SetActive(false);
        GetComponent<playerMovment>().enabled = true;
    }

    void LerpPositionToSitingSpot()
    {
        Vector3 endMarker = currentStachue.GetComponent<statue>().sittingSpot;

        float journeyLength = Vector3.Distance(transform.position, endMarker);
        float distCovered = (Time.time - startTime) * camSpeed;
        float fractionOfJourney = distCovered / journeyLength;

        transform.position = Vector3.Lerp(transform.position, currentStachue.GetComponent<statue>().sittingSpot, fractionOfJourney);
        
        Camera.main.transform.LookAt(currentStachue.transform.position);

        if(transform.position == endMarker && Camera.main.transform.position.y >= endMarker.y - 0.18f)
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - 0.005f, Camera.main.transform.position.z);
        }
    }
}
