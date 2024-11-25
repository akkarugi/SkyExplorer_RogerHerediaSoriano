using UnityEngine;

public class OpenChest : MonoBehaviour
{
    public Transform chestPiece; // La pieza del cofre que se moverá
    public float moveDistance = 0.5f; // Distancia a mover en el eje Z

    private Vector3 closedPosition; // Posición inicial
    private bool isOpen = false; // Estado del cofre (abierto/cerrado)

    void Start()
    {
        // Guarda la posición inicial
        closedPosition = chestPiece.position;
    }

    void Update()
    {
        // Detecta si el jugador presiona la tecla E
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isOpen)
            {
                // Si está abierto, regresa a la posición inicial
                chestPiece.position = closedPosition;
                isOpen = false;
            }
            else
            {
                // Si está cerrado, mueve hacia atrás en el eje Z
                chestPiece.position = closedPosition + new Vector3(0, 0, -moveDistance);
                isOpen = true;
            }
        }
    }
}
