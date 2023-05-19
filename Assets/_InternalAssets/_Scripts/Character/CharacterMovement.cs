using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private CharacterController characterController;

    private Vector2 movementInput;
    private Vector3 playerVelocity;

    private bool isGrounded = false;
    private bool isSprinting = false;

    private float speed = 5f;


    [SerializeField]
    private float movementSpeed = 5f;
    [SerializeField]
    private float sprintingSpeed = 8f;
    [SerializeField]
    private float gravity = -10f;
    [SerializeField]
    private float jumpHeight = 1f;

    public Vector2 MovementInput
    { 
        //get => movementInput;
        set
        {
            //Debug.Log(movementInput);
            movementInput = value;
        }
    }

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
            speed = sprintingSpeed;
        else
            speed = movementSpeed;
    }
}
