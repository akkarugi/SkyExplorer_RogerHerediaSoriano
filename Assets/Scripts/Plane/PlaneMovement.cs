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
    public float brakeRate = 0.5f;

    public float throttle;
    public float roll;
    public float pitch;
    public float yaw;

    public float windStrength = 5f;
    public float windTurbulence = 1f; 
    private float windYaw = 0f;
    private float windPitch = 0f;
    private float windRoll = 0f;

    public float GetThrottle()
    {
        return throttle / 100f;
    }

    public void SetThrottle(float newThrottle)
    {
        throttle = Mathf.Clamp(newThrottle, 0f, 100f);
    }

    private float responseModifier
    {
        get
        {
            return (rb.mass / 10f) * responsiveness;
        }
    }

    Rigidbody rb;

    [SerializeField] TextMeshProUGUI hud;
    [SerializeField] Transform propella;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
            throttle -= brakeRate * Time.deltaTime;

        throttle = Mathf.Clamp(throttle, 0f, 100f);
    }

    private void Update()
    {
        HandleInputs();
        UpdateHUD();
        propella.Rotate(Vector3.right * throttle);
    }

    private void FixedUpdate()
    {
     
        rb.AddForce(transform.forward * maxThrottle * throttle);
        rb.AddTorque(transform.up * yaw * responseModifier);
        rb.AddTorque(transform.right * pitch * responseModifier);
        rb.AddTorque(transform.forward * roll * responseModifier);

       
        ApplyWindEffect();

        
        rb.AddForce(Vector3.up * rb.velocity.magnitude * lift);
    }

    private void ApplyWindEffect()
    {
        
        windYaw = Random.Range(-windStrength, windStrength) * windTurbulence; 
        windPitch = Random.Range(-windStrength, windStrength) * windTurbulence; 
        windRoll = Random.Range(-windStrength, windStrength) * windTurbulence; 

       
        rb.AddTorque(transform.up * windYaw);    
        rb.AddTorque(transform.right * windPitch); 
        rb.AddTorque(transform.forward * windRoll); 
    }

    private void UpdateHUD()
    {
        hud.text = "Acceleration: " + throttle.ToString("F0") + "%\n";
        hud.text += "Speed: " + (rb.velocity.magnitude * 3.6f).ToString("F0") + " km/h\n";
        hud.text += "Altitude: " + transform.position.y.ToString("F0") + " m";
    }
}
