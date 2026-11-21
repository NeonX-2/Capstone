using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
        SceneManager.LoadScene("map");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}