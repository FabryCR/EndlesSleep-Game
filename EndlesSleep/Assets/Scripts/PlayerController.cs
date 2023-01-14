using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float walkSpeed;
    public float sprintSpeed;
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    [HideInInspector] public float moveSpeed;

    //JumpControls//bool readyToJump;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;
    public Transform orientation;


    [Header("Sprinting")]
    [SerializeField]
    float stamina;
    [SerializeField]
    float maxStamina = 100f;
    [SerializeField]
    Slider staminaBarSlider;
    [SerializeField]
    float staminaEmptyCooldown = 3f;
    [SerializeField]
    float staminaSpentPerFrame;
    [SerializeField]
    float staminaFillPerFrame;
    bool staminaIsEmpty = false;
    bool staminaIsLoading = false;
    float timeElapsed;

    [Header("Other")]
    [SerializeField]
    Transform light;
    [SerializeField]
    GameObject flashlightIcon;

    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    Rigidbody rb;

    [HideInInspector]
    public bool allowMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        //Stamina
        staminaBarSlider.maxValue = maxStamina;
        staminaBarSlider.value = maxStamina;
        stamina = maxStamina;

        //JumpControls//readyToJump = true;
    }

    void Update()
    {
        //GroundCheck
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();
        SpeedControl();

        //Drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        //Shift Running / Stamina

        //If Stamina = 0, then is Empty and starts loading....
        if (stamina <= 0)
        {
            staminaIsEmpty = true;
            staminaIsLoading = true;
        }
        else
        {
            staminaIsEmpty = false;
        }

        if (stamina == maxStamina)
        {
            staminaIsLoading = false;
        }

        //While is empty...
        if (staminaIsEmpty)
        {
            timeElapsed += Time.deltaTime;
        }

        //RefillStamina when stamina is not full and player is not sprinting
        if (stamina != maxStamina && moveSpeed != sprintSpeed)
        {
            ReFillStamina();
        }

        //Shift Input
        if (Input.GetKey(KeyCode.LeftShift))
            Sprint();
        else
            moveSpeed = walkSpeed;


        //Turn FlashLight
        if (Input.GetKeyDown(KeyCode.R))
        {
            light.gameObject.SetActive(!light.gameObject.active);
        }

        //Flashlight UI Icon
        flashlightIcon.SetActive(light.gameObject.active);
    }

    void Sprint()
    {
        if (staminaIsEmpty || staminaIsLoading)
        {
            moveSpeed = walkSpeed;
        }
        else
        {
            stamina -= staminaSpentPerFrame;
            staminaBarSlider.value = stamina;
            moveSpeed = sprintSpeed;
        }
    }

    void ReFillStamina()
    {
        //Stamina is 0... and cooldown ended....
        if (staminaIsEmpty && timeElapsed >= staminaEmptyCooldown)
        {
            timeElapsed = 0;
            staminaIsEmpty = false;
            stamina += staminaFillPerFrame;
            staminaBarSlider.value = stamina;
        }
        //Fill stamina
        else
        {
            stamina += staminaFillPerFrame;
            staminaBarSlider.value = stamina;

            //If stamina was loading and now is full...
            if (staminaIsLoading && stamina == maxStamina)
            {
                staminaIsLoading = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (allowMove)
        {
            MovePlayer();
        }
    }

    void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //JumpControls
        /*
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
        */
    }

    private void MovePlayer()
    {
        //Movimiento
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //Ground
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        //MidAir
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //Limitar Velocidad
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    //JumpControls
    /*private void Jump()
    {
        //Reset del velocity Y
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }

    */
}

