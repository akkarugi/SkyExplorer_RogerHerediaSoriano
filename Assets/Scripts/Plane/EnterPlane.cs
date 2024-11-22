using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enter_Plane : MonoBehaviour
{
    [SerializeField] private GameObject eSprite;
    [SerializeField] private PlaneController airplaneController;

    private bool isNearPlane = false;

    private void Start()
    {
        eSprite.SetActive(false); // Oculta el sprite al inicio
    }

    private void Update()
    {
        if (isNearPlane && Input.GetKeyDown(KeyCode.E))
        {
            airplaneController.TogglePlaneControl();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Plane"))
        {
            isNearPlane = true;
            eSprite.SetActive(true); // Activa el sprite
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Plane"))
        {
            isNearPlane = false;
            eSprite.SetActive(false); // Oculta el sprite
        }
    }
}