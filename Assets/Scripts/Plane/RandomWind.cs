using UnityEngine;

public class RandomWind : MonoBehaviour
{
    public PlaneMovement planeMovement; // Referencia al script PlaneMovement
    public float boostDuration = 5f; // Duración del boost
    public float boostAmount = 0.9f; // 20% más de fuerza
    public float interval = 3f; // Tiempo entre boosts

    private float timeSinceLastBoost = 0f; // Temporizador para intervalos
    private float boostTimer = 0f; // Temporizador para la duración del boost
    private bool isBoosting = false; // Indica si un boost está activo
    private int firstMove;
    private int secondMove;

    private float originalRoll;
    private float originalPitch;
    private float originalYaw;

    private void Update()
    {
        timeSinceLastBoost += Time.deltaTime;

        // Inicia un boost si el intervalo se ha cumplido
        if (!isBoosting && timeSinceLastBoost >= interval)
        {
            StartBoost();
        }

        // Maneja la duración del boost
        if (isBoosting)
        {
            boostTimer += Time.deltaTime;

            if (boostTimer >= boostDuration)
            {
                EndBoost();
            }
        }
    }

    private void StartBoost()
    {
        isBoosting = true;
        boostTimer = 0f;
        timeSinceLastBoost = 0f;

        // Seleccionar dos movimientos aleatorios
        firstMove = Random.Range(0, 3);
        do
        {
            secondMove = Random.Range(0, 3);
        } while (secondMove == firstMove);

        // Guardar los valores originales
        originalRoll = planeMovement.roll;
        originalPitch = planeMovement.pitch;
        originalYaw = planeMovement.yaw;

        // Aplicar boost a los movimientos seleccionados
        if (firstMove == 0 || secondMove == 0) planeMovement.roll += planeMovement.roll * boostAmount;
        if (firstMove == 1 || secondMove == 1) planeMovement.pitch += planeMovement.pitch * boostAmount;
        if (firstMove == 2 || secondMove == 2) planeMovement.yaw += planeMovement.yaw * boostAmount;
    }

    private void EndBoost()
    {
        isBoosting = false;

        // Restaurar los valores originales
        planeMovement.roll = originalRoll;
        planeMovement.pitch = originalPitch;
        planeMovement.yaw = originalYaw;
    }
}
