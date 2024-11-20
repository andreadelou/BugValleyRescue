using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    void Update()
    {
        // Detecta cualquier tecla presionada
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Carga la escena con índice 0
            SceneManager.LoadScene(0);
        }
    }
}
