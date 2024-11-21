using UnityEngine;
using TMPro; // Importa TextMeshPro
using UnityEngine.SceneManagement;

public class Puntos : MonoBehaviour
{
    public TextMeshProUGUI puntosText;
    public int puntosRequeridos = 10;
    private int puntos = 0;

    void Start()
    {
        UpdatePuntosText();
    }

    // Método para incrementar puntos
    public void IncrementarPuntos()
    {
        puntos++;
        UpdatePuntosText();

        // Verifica si los puntos han alcanzado el límite para cambiar de nivel
        if (puntos >= puntosRequeridos)
        {
            CambiarEscena();
        }
    }

    // Método para cambiar a la siguiente escena
    private void CambiarEscena()
    {
        int escenaActual = SceneManager.GetActiveScene().buildIndex; // Índice de la escena actual
        int siguienteEscena = escenaActual + 1; // Determina la siguiente escena

        if (siguienteEscena < SceneManager.sceneCountInBuildSettings) // Verifica que exista la siguiente escena
        {
            SceneManager.LoadScene(siguienteEscena); // Carga la siguiente escena
        }
        else
        {
            Debug.Log("No hay más escenas para cargar. Verifica las escenas en Build Settings.");
        }
    }

    // Método para actualizar el texto en pantalla
    private void UpdatePuntosText()
    {
        if (puntosText != null)
        {
            puntosText.text = "Puntos: " + puntos;
        }
        else
        {
            Debug.LogError("PuntosText no está asignado en el Inspector.");
        }
    }
}
