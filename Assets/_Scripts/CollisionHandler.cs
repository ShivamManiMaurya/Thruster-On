using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float loadDelay = 2f;

    Movement movement;

    private void Start()
    {
        movement = GetComponent<Movement>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Start":
                Debug.Log("Start is touched");
                break;
            case "Finish":
                StartLoadNextLevelSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartCrashSequence()
    {
        //todo sound effects on crash
        //todo particle effects on crash

        movement.enabled = false;

        Invoke("ReloadLevel", loadDelay);
    }

    private void StartLoadNextLevelSequence()
    {
        movement.enabled = false;

        Invoke("LoadNextLevel", loadDelay);
    }


    private void ReloadLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevelIndex);
    }

    private void LoadNextLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        if (SceneManager.sceneCountInBuildSettings == (currentLevelIndex + 1))
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(currentLevelIndex + 1);
        }
    }

}
