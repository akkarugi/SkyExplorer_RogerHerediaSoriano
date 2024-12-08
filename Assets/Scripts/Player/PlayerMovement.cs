using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintMultiplier = 1.2f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private Animator animator;
    [SerializeField] private Slider staminaBar;
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float staminaDepletionRate = 20f;
    [SerializeField] private float staminaRecoveryRate = 15f;
    private Rigidbody rb;
    private float currentStamina;
    private bool canSprint = true;
    private bool isGrounded = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = currentStamina;
    }

    private void FixedUpdate()
    {
        HandleMovement();
        UpdateStamina();
        UpdateAnimations();
    }

    private void Update()
    {
        HandleJump();
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        bool isSprinting = Input.GetKey(KeyCode.LeftShift) && canSprint;
        float currentSpeed = isSprinting ? moveSpeed * sprintMultiplier : moveSpeed;

        Vector3 direction = transform.forward * vertical + transform.right * horizontal;
        Vector3 movement = direction.normalized * currentSpeed * Time.deltaTime;

        transform.position += movement;

        float movementMagnitude = new Vector2(horizontal, vertical).magnitude;
        animator.SetFloat("MoveSpeed", movementMagnitude, 0.1f, Time.deltaTime);

        if (isSprinting && movementMagnitude > 0)
        {
            currentStamina -= staminaDepletionRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
            if (currentStamina <= 0)
            {
                canSprint = false;
            }
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void UpdateStamina()
    {
        if (!Input.GetKey(KeyCode.LeftShift) || !canSprint)
        {
            currentStamina += staminaRecoveryRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
            if (currentStamina >= maxStamina)
            {
                canSprint = true;
            }
        }

        staminaBar.value = currentStamina;
    }

    private void UpdateAnimations()
    {
        animator.SetBool("Grounded", isGrounded);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }
    }
}
