using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitApplication : MonoBehaviour
{
    private void Update()
    {
        QuitGame();
    }

    private void QuitGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("you quit");
        }
    }
}
