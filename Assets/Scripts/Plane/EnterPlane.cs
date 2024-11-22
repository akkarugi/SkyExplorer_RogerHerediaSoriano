using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterExit_Plane : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject eSprite;
    [SerializeField] private Transform planeExitPoint;

    private bool isNearPlane = false;
    private bool isPlayerInPlane = false;

    private void Start()
    {
        eSprite.SetActive(false); // Asegúrate de que el sprite esté apagado al inicio.
    }

    private void Update()
    {
        if (isNearPlane && Input.GetKeyDown(KeyCode.E))
        {
            if (!isPlayerInPlane)
            {
                EnterPlane();
            }
            else
            {
                ExitPlane();
            }
        }
    }

    private void EnterPlane()
    {
        isPlayerInPlane = true;
        player.SetActive(false); // Desactiva el jugador (incluidos sus scripts).
        eSprite.SetActive(false); // Oculta el sprite.
    }

    private void ExitPlane()
    {
        isPlayerInPlane = false;
        player.SetActive(true); // Reactiva el jugador.
        player.transform.position = planeExitPoint.position; // Coloca al jugador cerca de la avioneta.
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearPlane = true;
            eSprite.SetActive(true); // Activa el sprite para mostrar la letra "E".
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearPlane = false;
            eSprite.SetActive(false); // Desactiva el sprite al salir del rango.
        }
    }
}
