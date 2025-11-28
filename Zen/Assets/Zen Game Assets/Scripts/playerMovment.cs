using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class playerMovment : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    private string horizontalAxis = "Horizontal";
    private string verticalAxis = "Vertical";

    [Header("Look")]
    public Camera playerCamera;
    public float mouseLookSensitivity = 2.0f;      // multiplier for mouse delta (degrees per mouse unit)
    public float controllerLookSensitivity = 180f; // degrees per second at full stick
    public bool invertY = false;
    private float pitchMin = -89f;
    private float pitchMax = 89f;


    private string rightStickHorizontalAxis = "RightStickHorizontal";
    private string rightStickVerticalAxis = "RightStickVertical";


    public bool lockCursor = true;

    // internals
    Rigidbody rb;
    float currentYaw = 0f;
    float currentPitch = 0f;
    public Vector3 velocityInput = Vector3.zero;


    Vector2 smoothedControllerStick = Vector2.zero;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Update()
    {

        float horizontal = Input.GetAxis(horizontalAxis);
        float vertical = Input.GetAxis(verticalAxis);


        Vector3 inputLocal = new Vector3(horizontal, 0f, vertical);
        inputLocal = Vector3.ClampMagnitude(inputLocal, 1f);
        velocityInput = transform.TransformDirection(inputLocal) * moveSpeed;


        float rawMouseX = Input.GetAxis("Mouse X");
        float rawMouseY = Input.GetAxis("Mouse Y");


        currentYaw += rawMouseX * mouseLookSensitivity;
        currentPitch += (invertY ? rawMouseY : -rawMouseY) * mouseLookSensitivity;


        float rsX = 0f;
        float rsY = 0f;
        TryReadLegacyRightStick(ref rsX, ref rsY);

        float controllerYawDelta = rsX * controllerLookSensitivity * Time.deltaTime;
        float controllerPitchDelta = (invertY ? rsY : -rsY) * controllerLookSensitivity * Time.deltaTime;

        currentYaw += controllerYawDelta;
        currentPitch += controllerPitchDelta;

      

        currentPitch = Mathf.Clamp(currentPitch, pitchMin, pitchMax);
        playerCamera.transform.localRotation = Quaternion.Euler(currentPitch, 0f, 0f);
    }

    void FixedUpdate()
    {
        Vector3 newPosition = rb.position + velocityInput * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);

        Quaternion targetRotation = Quaternion.Euler(0f, currentYaw, 0f);
        rb.MoveRotation(targetRotation);
    }

    private void TryReadLegacyRightStick(ref float outX, ref float outY)
    {
        outX = 0f;
        outY = 0f;
        
        outX = Input.GetAxis(rightStickHorizontalAxis);
        outY = Input.GetAxis(rightStickVerticalAxis);
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}

