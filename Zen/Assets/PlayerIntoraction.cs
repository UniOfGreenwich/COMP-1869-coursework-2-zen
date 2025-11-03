using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class PlayerIntoraction : MonoBehaviour
{
    public GameObject UI_Einteract, BreethBar, currentStachue, hand;
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
        if ((other.gameObject.tag == "statue" && E_flashActive == false && !other.GetComponent<statue>().haveBeenSelected) && hand.GetComponentInChildren<statue>() == null)
        {
            StartCoroutine(E_flash());
            currentStachue = other.gameObject;
        }

        if ((Input.GetKey(KeyCode.E) || Input.GetKeyDown(KeyCode.Joystick1Button0)) && !other.GetComponent<statue>().haveBeenSelected && hand.GetComponentInChildren<statue>() == null)
        { 
            StartCoroutine(E_flashSelected()); 
            other.GetComponent<statue>().haveBeenSelected = true; 
        }

    }
    private void Update()
    {
        if(Input.GetKey(KeyCode.E) || Input.GetKeyDown(KeyCode.Joystick1Button0) && hand.GetComponentInChildren<statue>() != null)
        {
            print("hi");
            currentStachue.transform.SetParent(null);
        }
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
            yield return new WaitForSeconds(9);

            BreethBar.SetActive(true);

            GetComponent<CapsuleCollider>().enabled = true;  
            GetComponent<Rigidbody>().useGravity = true;  

            UI_Einteract.GetComponent<Image>().color = Color.white;
            sat = false;

            Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y + 0.45f,transform.position.z);
            BreethBar.SetActive(false);
            GetComponent<playerMovment>().enabled = true;

            currentStachue.transform.SetParent(hand.transform);
            currentStachue.transform.localPosition = Vector3.zero;
            currentStachue.transform.localRotation = Quaternion.identity;
        
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
