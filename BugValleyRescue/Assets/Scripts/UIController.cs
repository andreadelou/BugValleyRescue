using UnityEngine;
using TMPro; // Importa TextMeshPro
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI faltasText; // Cambia a TextMeshProUGUI
    private int faltas = 0;

    void Start()
    {
        UpdateFaltasText();
    }

    public void IncrementarFaltas()
    {
        faltas++;
        UpdateFaltasText();

        if (faltas >= 3)
        {
            SceneManager.LoadSceneAsync(0);
        }
    }

    private void UpdateFaltasText()
    {
        faltasText.text = "Faltas: " + faltas;
    }
}
