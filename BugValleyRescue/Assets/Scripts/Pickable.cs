using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    private bool isCaptured = false;
    private float fadeDuration = 5.0f; // Duración del desvanecimiento

    // Método que captura al insecto y comienza el desvanecimiento
    public void Capture()
    {
        if (!isCaptured)
        {
            isCaptured = true;
            StartCoroutine(FadeOutAndDisable());
        }
    }

    // Corutina para desvanecer y desactivar el insecto
    private IEnumerator FadeOutAndDisable()
    {
        // Obtiene el material del insecto (suponiendo que tiene un Renderer)
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            Color originalColor = renderer.material.color;
            float fadeSpeed = 1.0f / fadeDuration;
            float progress = 0f;

            // Desvanecer el color del insecto reduciendo el alfa
            while (progress < 1.0f)
            {
                progress += Time.deltaTime * fadeSpeed;
                Color color = originalColor;
                color.a = Mathf.Lerp(1.0f, 0.0f, progress);
                renderer.material.color = color;
                yield return null;
            }
        }

        // Aquí desactivamos el objeto después de completar el desvanecimiento
        gameObject.SetActive(false);
    }

    // Comprobar si el jugador está cerca para capturar
    public bool IsPlayerNearby(Transform playerTransform, float captureDistance)
    {
        return Vector3.Distance(transform.position, playerTransform.position) <= captureDistance;
    }
}
