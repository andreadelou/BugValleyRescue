using System.Collections;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    private bool isCaptured = false;
    private float fadeDuration = 5.0f; // Duración del desvanecimiento

    private Puntos puntosController; // Referencia al script Puntos

    void Start()
    {
        // Encuentra el componente Puntos en la escena
        puntosController = FindObjectOfType<Puntos>();
        if (puntosController == null)
        {
            Debug.LogError("Puntos no encontrado en la escena. Asegúrate de que el objeto que contiene Puntos está en la escena.");
        }
    }

    // Método que captura al insecto y comienza el desvanecimiento
    public void Capture()
    {
        if (!isCaptured)
        {
            isCaptured = true;
            StartCoroutine(FadeOutAndDisable());

            // Incrementa los puntos en el controlador de puntos
            if (puntosController != null)
            {
                puntosController.IncrementarPuntos();
            }
        }
    }

    // Corutina para desvanecer y desactivar el insecto
    private IEnumerator FadeOutAndDisable()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            Color originalColor = renderer.material.color;
            float fadeSpeed = 1.0f / fadeDuration;
            float progress = 0f;

            // Desvanece el color del insecto reduciendo el alfa
            while (progress < 1.0f)
            {
                progress += Time.deltaTime * fadeSpeed;
                Color color = originalColor;
                color.a = Mathf.Lerp(1.0f, 0.0f, progress);
                renderer.material.color = color;
                yield return null;
            }
        }

        // Desactiva el objeto después de completar el desvanecimiento
        gameObject.SetActive(false);
    }

    // Comprobar si el jugador está cerca para capturar
    public bool IsPlayerNearby(Transform playerTransform, float captureDistance)
    {
        return Vector3.Distance(transform.position, playerTransform.position) <= captureDistance;
    }
}
