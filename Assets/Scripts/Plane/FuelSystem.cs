using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FuelSystem : MonoBehaviour
{
    public float maxFuel = 100f;
    public float baseFuelConsumptionRate = 0.1f;
    public Slider fuelSlider;
    [SerializeField] TextMeshProUGUI fuelText;

    private float currentFuel;
    private bool isConsumingFuel = true;
    private PlaneMovement planeMovement;

    public float throttleDecreaseRate = 0.5f;

    private void Start()
    {
        currentFuel = maxFuel;
        planeMovement = GetComponent<PlaneMovement>();
        UpdateFuelUI();
    }

    private void Update()
    {
        if (currentFuel > 0 && isConsumingFuel)
        {
            ConsumeFuel();
        }
        else if (currentFuel <= 0)
        {
            currentFuel = 0;
            StopPlane();
            DecreaseThrottle();
        }

        if (Input.GetKeyDown(KeyCode.Z)) 
        {
            RefillFuel();
        }
    }

    public void StartConsumingFuel()
    {
        isConsumingFuel = true;
    }

    public void StopConsumingFuel()
    {
        isConsumingFuel = false;
    }

    private void ConsumeFuel()
    {
        float throttle = planeMovement.GetThrottle();
        float adjustedFuelConsumptionRate = baseFuelConsumptionRate * throttle;
        currentFuel -= adjustedFuelConsumptionRate * Time.deltaTime;
        currentFuel = Mathf.Clamp(currentFuel, 0, maxFuel);
        UpdateFuelUI();
    }

    private void DecreaseThrottle()
    {
        if (planeMovement != null)
        {
            planeMovement.SetThrottle(planeMovement.GetThrottle() - throttleDecreaseRate * Time.deltaTime);
            planeMovement.SetThrottle(Mathf.Max(planeMovement.GetThrottle(), 0f));
        }
    }

    public void RefillFuel()
    {
        currentFuel = maxFuel;
        UpdateFuelUI();
    }

    private void UpdateFuelUI()
    {
        fuelSlider.value = currentFuel / maxFuel;
        fuelText.text = "Fuel: " + Mathf.Round(currentFuel).ToString() + " L";
    }

    private void StopPlane()
    {
        Debug.Log("Fuel is empty! The plane has stopped.");
    }
}