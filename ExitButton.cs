using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
{
    public void ExitGame()
    {
        Application.Quit();
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");

    }
}
