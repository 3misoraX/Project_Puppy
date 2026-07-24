using Unity.VisualScripting;
using UnityEditor.Compilation;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    private CharacterController player;
    [SerializeField] private Transform cameraTransform;
    [Header("Movement Options")]
    [SerializeField] private Vector2 moveInput;
    public float speed;
    [Header("Jump Options")]
    public float gravity;
    public float jumpForce;
    public float fallSpeed;
    private float verticalSpeed;
    private bool isGrounded = true;

    //nombreDeLaAccion.action.triggered = true  para botones
    //nombreDeLaAccion.action.ReadValue<TipoDeDato>();  para valores
    
    //Stores movement input
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void Awake()
    {
        player = GetComponent<CharacterController>();
    }
    
    void Update()
    {
        isGrounded = player.isGrounded;
        //Function that handles gravity
        HandleGravity();
        //Function that handles movement
        Movement();
        //Rotates the player forward towards the camera forward ignoring the y axis
        transform.forward = new Vector3(cameraTransform.forward.x, 0, cameraTransform.forward.z);
    }
    
    //Handles all movement
    void Movement()
    {
        Vector3 mover = cameraTransform.TransformDirection(new Vector3(moveInput.x, 0, moveInput.y)).normalized;
        mover *= speed;
        mover.y = verticalSpeed;
        player.Move(mover * Time.deltaTime);
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
        }
    }
}
