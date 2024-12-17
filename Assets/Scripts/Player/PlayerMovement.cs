using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintMultiplier = 1.2f;
    [SerializeField] private float jumpForce = 7f;

    [Header("Resistencia")]
    [SerializeField] private Slider staminaBar;
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float staminaDepletionRate = 20f;
    [SerializeField] private float staminaRecoveryRate = 15f;

    [Header("Animación y Físicas")]
    [SerializeField] private Animator animator;
    private Rigidbody rb;

    [Header("Audio")]
    [SerializeField] private AudioSource movementAudioSource;
    [SerializeField] private AudioSource effectsAudioSource;
    [SerializeField] private AudioClip walkClip;
    [SerializeField] private AudioClip runClip;
    [SerializeField] private AudioClip noStaminaClip;
    [SerializeField] private AudioClip jumpClip;

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
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        bool isSprinting = Input.GetKey(KeyCode.LeftShift) && canSprint;
        float currentSpeed = isSprinting ? moveSpeed * sprintMultiplier : moveSpeed;

        Vector3 direction = (transform.forward * vertical + transform.right * horizontal).normalized;

        if (direction.magnitude > 0)
        {
            rb.velocity = new Vector3(direction.x * currentSpeed, rb.velocity.y, direction.z * currentSpeed);
            PlayMovementAudio(isSprinting);
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            StopMovementAudio();
        }

        if (isSprinting && direction.magnitude > 0)
        {
            DepleteStamina();
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            isGrounded = false;
            effectsAudioSource.PlayOneShot(jumpClip);
        }
    }

    private void UpdateStamina()
    {
        if (!Input.GetKey(KeyCode.LeftShift) || !canSprint)
        {
            RecoverStamina();
        }

        staminaBar.value = currentStamina;
    }

    private void UpdateAnimations()
    {
        animator.SetBool("Grounded", isGrounded);
    }

    private void DepleteStamina()
    {
        currentStamina -= staminaDepletionRate * Time.deltaTime;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

        if (currentStamina <= 0 && canSprint)
        {
            canSprint = false;
            PlayNoStaminaAudio();
        }
    }

    private void RecoverStamina()
    {
        currentStamina += staminaRecoveryRate * Time.deltaTime;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

        if (currentStamina >= maxStamina)
        {
            canSprint = true;
            StopNoStaminaAudio();
        }
    }

    private void PlayMovementAudio(bool isSprinting)
    {
        AudioClip clipToPlay = isSprinting ? runClip : walkClip;

        if (movementAudioSource.clip != clipToPlay || !movementAudioSource.isPlaying)
        {
            movementAudioSource.clip = clipToPlay;
            movementAudioSource.loop = true;
            movementAudioSource.Play();
        }
    }

    private void StopMovementAudio()
    {
        if (movementAudioSource.isPlaying)
        {
            movementAudioSource.Stop();
        }
    }

    private void PlayNoStaminaAudio()
    {
        if (!effectsAudioSource.isPlaying || effectsAudioSource.clip != noStaminaClip)
        {
            effectsAudioSource.clip = noStaminaClip;
            effectsAudioSource.loop = true;
            effectsAudioSource.Play();
        }
    }

    private void StopNoStaminaAudio()
    {
        if (effectsAudioSource.isPlaying && effectsAudioSource.clip == noStaminaClip)
        {
            effectsAudioSource.Stop();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }
    }
}
