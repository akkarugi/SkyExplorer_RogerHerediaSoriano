using System.Collections;
using UnityEngine;

public class StormForce : MonoBehaviour
{
    public float windForceStrength = 10f; // Magnitud máxima de la fuerza del viento
    public float forceInterval = 0.1f; // Intervalo entre las fuerzas (segundos)

    private Coroutine stormCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        // Inicia la tormenta si el avión entra en la nube
        if (other.CompareTag("Plane"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                stormCoroutine = StartCoroutine(ApplyStormEffect(rb));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Detiene la tormenta cuando el avión sale de la nube
        if (other.CompareTag("Player") && stormCoroutine != null)
        {
            StopCoroutine(stormCoroutine);
        }
    }

    private IEnumerator ApplyStormEffect(Rigidbody rb)
    {
        while (true)
        {
            // Genera fuerzas aleatorias en los ejes X, Y, Z
            Vector3 windForce = new Vector3(
                Random.Range(-windForceStrength, windForceStrength), // Fuerza en X
                Random.Range(-windForceStrength, windForceStrength), // Fuerza en Y
                Random.Range(-windForceStrength, windForceStrength)  // Fuerza en Z
            );

            // Aplica la fuerza al avión
            rb.AddForce(windForce, ForceMode.Force);

            // Espera el intervalo antes de aplicar la siguiente fuerza
            yield return new WaitForSeconds(forceInterval);
        }
    }
}
