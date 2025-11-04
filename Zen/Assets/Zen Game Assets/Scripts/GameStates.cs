using UnityEngine;

public class GameStates : MonoBehaviour
{
    public static GameStates Instance;
    public GameObject mainLight;
    public int amountodStatues;
    float light = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(amountodStatues >= 5)
        {
            mainLight.GetComponent<Light>().intensity += light;
            light = light * 1.1f;
        }
    }
}
