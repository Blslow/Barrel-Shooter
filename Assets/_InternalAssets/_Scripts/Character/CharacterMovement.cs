using UnityEngine;
using UnityEngine.Events;

public class CharacterMovement : MonoBehaviour
{
    private CharacterController characterController;

    private Vector2 movementInput;
    private Vector3 playerVelocity;

    private bool isGrounded = false;
    private bool isSprinting = false;
    //private bool isCrouching = false;

    private float speed = 5f;


    [SerializeField]
    private float movementSpeed = 5f;
    [SerializeField]
    private float sprintIncreaseMovementSpeedBy = 3f;
    [SerializeField]
    private float crouchDecreaseMovementSpeedBy = 3f;
    [SerializeField]
    private float gravity = -10f;
    [SerializeField]
    private float jumpHeight = 1f;

    //[Header("Events")]
    //[SerializeField]
    //private UnityEvent<bool> OnSprint;

    public Vector2 MovementInput
    { 
        //get => movementInput;
        set
        {
            //Debug.Log(movementInput);
            movementInput = value;
        }
    }

    //public bool IsCrouching { get => isCrouching; set => isCrouching = value; }

    private void Start()
    {
        speed = movementSpeed;
    }

    private void Awake()
    {
        characterController = GetComponentInParent<CharacterController>();
    }

    private void Update()
    {
        isGrounded = characterController.isGrounded;
    }

    private void FixedUpdate()
    {
        HandleMovement(movementInput);
    }

    private void HandleMovement(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        characterController.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;

        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;

        characterController.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (isGrounded)
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3f * gravity);
    }

    public void Sprint()
    {
        isSprinting = !isSprinting;

        if (isSprinting)
            speed += sprintIncreaseMovementSpeedBy;
        else
            speed -= sprintIncreaseMovementSpeedBy;
    }

    public void ToggleCrouchSpeed(bool isCrouching)
    {
        if (isCrouching)
            speed -= crouchDecreaseMovementSpeedBy;
        else
            speed += crouchDecreaseMovementSpeedBy;

    }
}
