using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float loadDelay = 1f, deathDelay = 3f;
    [SerializeField] private AudioClip sucessClip, explosionClip;
    [Range(0, 1)]
    [SerializeField] private float explosionVolume, sucessVolume;
    [SerializeField] private ParticleSystem explosion, sucess;

    Movement movement;
    AudioSource audioSource;
    
 
    private bool isTransitioning = false;
    private bool collisionDisabled = false;

    private void Start()
    {
        movement = GetComponent<Movement>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RespondToDebugKey();
    }

    private void RespondToDebugKey()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartLoadNextLevelSequence();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;  // toggle collision
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || collisionDisabled) { return; }

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
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(explosionClip, explosionVolume);
        explosion.Play();

        movement.enabled = false;
        Invoke("ReloadLevel", deathDelay);
    }

    private void StartLoadNextLevelSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(sucessClip, sucessVolume);
        sucess.Play();

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
