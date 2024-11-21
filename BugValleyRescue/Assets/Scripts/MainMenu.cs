using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void Credits()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void Principal()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void Level2()
    {
        SceneManager.LoadSceneAsync(3);
    }

}
