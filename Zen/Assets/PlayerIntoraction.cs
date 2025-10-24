using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerIntoraction : MonoBehaviour
{
    public GameObject UI_Einteract, BreethBar;
    bool E_flashActive;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "statue" && E_flashActive == false && !other.GetComponent<statue>().haveBeenSelected)
        {
            StartCoroutine(E_flash());
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
        StatueSit();
    }

    void StatueSit()
    {
        GetComponent<playerMovment>().moveSpeed = 0;
        Camera.main.transform.position = new Vector3(0, 1, 0);
        BreethBar.SetActive(true);
    }
}
