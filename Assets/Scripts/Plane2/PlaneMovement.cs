using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlaneMovement : MonoBehaviour
{
    public float throttleIncrement = 0.1f;
    public float maxThrottle = 200f;
    public float responsiveness = 10f;
    public float lift = 135f;
    public float brakeRate = 0.5f; // Tasa de frenado al presionar C

    private float throttle;
    private float roll;
    private float pitch;
    private float yaw;

    private float responseModifier
    {
        get
        {
            return (rb.mass / 10f) * responsiveness;
        }
    }

    Rigidbody rb;
    AudioSource engineSound;

    [SerializeField] TextMeshProUGUI hud;
    [SerializeField] Transform propella;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        engineSound = GetComponent<AudioSource>();
    }

    private void HandleInputs()
    {
        roll = Input.GetAxis("Roll");
        pitch = Input.GetAxis("Pitch");
        yaw = Input.GetAxis("Yaw");

        if (Input.GetKey(KeyCode.Space))
            throttle += throttleIncrement;
        else if (Input.GetKey(KeyCode.LeftControl))
            throttle -= throttleIncrement;
        else if (Input.GetKey(KeyCode.C))
            throttle -= brakeRate * Time.deltaTime; // Reduce gradualmente al presionar C

        throttle = Mathf.Clamp(throttle, 0f, 100f);
    }

    private void Update()
    {
        HandleInputs();
        UpdateHUD();

        propella.Rotate(Vector3.right * throttle);
        engineSound.volume = throttle * 0.01f;
    }

    private void FixedUpdate()
    {
        rb.AddForce(transform.forward * maxThrottle * throttle);
        rb.AddTorque(transform.up * yaw * responseModifier);
        rb.AddTorque(transform.right * pitch * responseModifier);
        rb.AddTorque(transform.forward * roll * responseModifier);

        rb.AddForce(Vector3.up * rb.velocity.magnitude * lift);
    }

    private void UpdateHUD()
    {
        hud.text = "Throttle: " + throttle.ToString("F0") + "%\n";
        hud.text += "Speed: " + (rb.velocity.magnitude * 3.6f).ToString("F0") + " km/h\n";
        hud.text += "Altitude: " + transform.position.y.ToString("F0") + " m";
    }
}
