using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    //CHARACTER MOVEMENT
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;

    //CAMERA MOVEMENT
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    //JUMP AND CROUCH
    public float defaultHeight = 2f;
    public float crouchHeight = 1f;
    public float crouchSpeed = 3f;

    //MOVEMENT AND CONTROL
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController characterController;
    public bool canMove = true;
    public bool isAiming = true;

    void Start()
    {
        //CONTROLS PLAYER AND DISABLEWS CURSOR
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (canMove)
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            //MOVEMENT KEYS////////////////////////////////////////////////////////////////////////////////////////////////////
            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            float curSpeedX = (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical");
            float curSpeedY = (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal");
            float movementDirectionY = moveDirection.y;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            if (Input.GetButton("Jump") && characterController.isGrounded)
            {
                moveDirection.y = jumpPower;
            }
            else
            {
                moveDirection.y = movementDirectionY;
            }

            if (!characterController.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }

            //crouch
            if (Input.GetKey(KeyCode.LeftControl))
            {
                characterController.height = crouchHeight;
                walkSpeed = crouchSpeed;
                runSpeed = crouchSpeed;
            }
            else
            {
                characterController.height = defaultHeight;
                walkSpeed = 6f;
                runSpeed = 12f;
            }

            characterController.Move(moveDirection * Time.deltaTime);
        }
    }

    //AIMINGGGG//////////////////////////////////////////// WE HAVE CROSSHAIR HERHER
    public void OnAiming(InputValue value)
    {
        isAiming = value.isPressed;
    }
}