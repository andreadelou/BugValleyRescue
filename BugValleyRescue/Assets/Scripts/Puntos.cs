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
            SceneManager.LoadScene(2); 
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
