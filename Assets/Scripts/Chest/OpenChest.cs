using UnityEngine;

public class OpenChest : MonoBehaviour
{
    public Transform chestPiece; // La pieza del cofre que se mover�
    public float moveDistance = 0.5f; // Distancia a mover en el eje Z

    private Vector3 closedPosition; // Posici�n inicial
    private bool isOpen = false; // Estado del cofre (abierto/cerrado)

    void Start()
    {
        // Guarda la posici�n inicial
        closedPosition = chestPiece.position;
    }

    void Update()
    {
        // Detecta si el jugador presiona la tecla E
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isOpen)
            {
                // Si est� abierto, regresa a la posici�n inicial
                chestPiece.position = closedPosition;
                isOpen = false;
            }
            else
            {
                // Si est� cerrado, mueve hacia atr�s en el eje Z
                chestPiece.position = closedPosition + new Vector3(0, 0, -moveDistance);
                isOpen = true;
            }
        }
    }
}
