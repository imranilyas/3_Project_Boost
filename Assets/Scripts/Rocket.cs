using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float levelLoadDelay = 2f;

    // Audio for different states
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip clearLevel;
    [SerializeField] AudioClip gameOver;

    // ParticleSystem for different states
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem clearLevelParticles;
    [SerializeField] ParticleSystem gameOverParticles;

    Rigidbody rigidBody;
    AudioSource audio;

    enum GameState { Alive, Dying, Transition};
    GameState state = GameState.Alive;

    // Counters for keycommands
    bool trigger = true;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {//TODO: stop sound upon death
        if (state == GameState.Alive)
        {
            RespondToThrust();
            RespondToRotation();
        }

        // key command for loading next scene
        if (Input.GetKeyDown(KeyCode.L))
        {
            print("Skip Level");
            LoadNextScene();
        }

        // turn off/on collision detection
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (trigger == true)
            {
                rigidBody.detectCollisions = false;
                trigger = false;
            }
            else
            {
                rigidBody.detectCollisions = true;
                trigger = true;
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (state != GameState.Alive)
        {
            return;
        }
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                print("Level Cleared");
                audio.PlayOneShot(clearLevel);
                clearLevelParticles.Play();
                state = GameState.Transition;
                Invoke("LoadNextScene", levelLoadDelay);
                break;
            default:
                print("Game Over");
                audio.PlayOneShot(gameOver);
                gameOverParticles.Play();
                state = GameState.Dying;
                Invoke("LoadFirstScene", levelLoadDelay);
                break;
        }
    }

    private void LoadFirstScene()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int lastScene = SceneManager.sceneCountInBuildSettings;
        currentScene++;
        if (currentScene == lastScene)
        {
            // repeats last scene
            LoadFirstScene();
        }
        else
        {
            SceneManager.LoadScene(currentScene);
        }
    }

    private void RespondToThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        {
            audio.Stop();
            mainEngineParticles.Stop();
        }
    }

    private void ApplyThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audio.isPlaying)
        {
            audio.PlayOneShot(mainEngine);
        }
        mainEngineParticles.Play();
    }

    private void RespondToRotation()
    {
        rigidBody.angularVelocity = Vector3.zero; // remove rotation to fix rotation
        float rotationThisFrame = Time.deltaTime * rcsThrust;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.back * rotationThisFrame);
        }
    }
}
