using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI faltasText; 
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
