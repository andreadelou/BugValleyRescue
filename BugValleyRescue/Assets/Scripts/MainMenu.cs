using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void Credits()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void Principal()
    {
        SceneManager.LoadSceneAsync(0);
    }

}
