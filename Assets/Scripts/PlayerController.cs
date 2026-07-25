using System.Collections.Generic;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    private CharacterController player;
    [Header("Camera")]
    [SerializeField] private Transform cameraTransform;
    public GameObject cameraObject;
    public int mouseSensibility = 10;
    public bool invertY = false;
    [Header("Movement Options")]
    [SerializeField] private Vector2 moveInput;
    public float speed;
    [Header("Jump Options")]
    public float gravity;
    public float jumpForce;
    public float fallSpeed;
    private float verticalSpeed;
    public bool isGrounded = true;
    [Header("Detective Mode")]
    public bool detectiveMode = false;
    public float duration;
    public GameObject detectiveCamera;
    public List<GameObject> objectiveList;
    public float cooldown;
    [Header("SFX")]
    private AudioSource source;
    public AudioClip walkFx;
    public AudioSource SFXSource;
    public AudioClip activeFx;
    private float delay = 0f;
    

    private void Awake()
    {
        player = GetComponent<CharacterController>();
        ChangeSensibility(cameraObject);
        ChangeSensibility(detectiveCamera);
        source = GetComponent<AudioSource>();
        source.clip = walkFx;
    }

    private void Start()
    {
        objectiveList.AddRange(GameObject.FindGameObjectsWithTag("Objectives"));
        delay = 0;
    }

    void Update()
    {
        isGrounded = player.isGrounded;
        //Function that handles gravity
        HandleGravity();
        //Function that handles movement
        Movement();
        WalkingSFX();
        //Rotates the player forward towards the camera forward ignoring the y axis
        transform.forward = new Vector3(cameraTransform.forward.x, 0, cameraTransform.forward.z);

        if(cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
    }

    //This function will change the sensibility in the mouse
    public void ChangeSensibility(GameObject activeCamera)
    {
        CinemachineInputAxisController inputAxis = activeCamera.GetComponent<CinemachineInputAxisController>();
        foreach(var controller in inputAxis.Controllers)
        {
            if(controller.Name == "Look Orbit X" || controller.Name == "Look X (Pan)")
            {
                controller.Input.Gain = mouseSensibility;
            }
            else if(controller.Name == "Look Orbit Y" || controller.Name == "Look Y (Tilt)")
            {
                if (!invertY)
                {
                    controller.Input.Gain = -mouseSensibility;
                }
                else
                {
                    controller.Input.Gain = mouseSensibility;
                }
            }
        }
    }

    //Stores movement input
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    
    //Handles all movement
    void Movement()
    {
        Vector3 mover = cameraTransform.TransformDirection(new Vector3(moveInput.x, 0, moveInput.y)).normalized;
        mover *= speed;
        mover.y = verticalSpeed;
        player.Move(mover * Time.deltaTime);
    }
    void WalkingSFX()
    {
        if(moveInput != Vector2.zero && isGrounded)
        {
            if(!source.isPlaying)
            {
                source.PlayDelayed(delay);
                if (detectiveMode) delay = 0.3f;
            }
        }
        else
        {
            source.Stop();
            delay = 0f;
        }
    }

    //to manage gravity and stuff
    void HandleGravity()
    {
        if (isGrounded && verticalSpeed < 0)
        {
            verticalSpeed = fallSpeed;
        }

        verticalSpeed += gravity * Time.deltaTime;
    }

    //Jumps on command
    void OnJump(InputValue value)
    {
        if (isGrounded)
        {
            verticalSpeed = jumpForce;
            source.Stop();
        }
    }

    //Changes camera from first person to third person
    void CameraChange()
    {
        if (detectiveMode)
        {
            detectiveCamera.GetComponent<CinemachineCamera>().enabled = true;
            cameraObject.GetComponent<CinemachineCamera>().enabled = false;
        }
        else
        {
            cameraObject.GetComponent<CinemachineCamera>().enabled = true;
            detectiveCamera.GetComponent<CinemachineCamera>().enabled = false;
        }
    }

    void OnDetectiveMode(InputValue value)
    {
        if(!detectiveMode && cooldown <= 0)
        {
            SFXSource.clip = activeFx;
            SFXSource.Play();
            StartCoroutine(ActivateDetectiveMode());
        }
    }

    IEnumerator ActivateDetectiveMode()
    {
        Light indicator;
        detectiveMode = true;
        CameraChange();
        speed /= 4;
        GameObject.Find("Directional Light").GetComponent<Light>().intensity = 0;
        foreach(GameObject person in objectiveList)
        {
            person.TryGetComponent<Light>(out indicator);
            if(indicator != null)
            {
                indicator.enabled = true;
            }
        }

        yield return new WaitForSeconds(duration);

        GameObject.Find("Directional Light").GetComponent<Light>().intensity = 1;
        foreach (GameObject person in objectiveList)
        {
            person.TryGetComponent<Light>(out indicator);
            if (indicator != null)
            {
                indicator.enabled = false;
            }
        }
        detectiveMode = false;
        CameraChange();
        speed *= 4;
        cooldown = duration;
    }

    //Aparecen los indicadores (fuentes de luz)
    //reducir velocidad
}
