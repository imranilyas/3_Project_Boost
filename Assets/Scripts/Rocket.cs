using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;

    //Audio for different states
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip clearLevel;
    [SerializeField] AudioClip gameOver;

    //ParticleSystem for different states
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem clearLevelParticles;
    [SerializeField] ParticleSystem gameOverParticles;

    Rigidbody rigidBody;
    AudioSource audio;

    enum GameState { Alive, Dying, Transition};
    GameState state = GameState.Alive;

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
                Invoke("LoadNextScene", 1.5f);
                break;
            default:
                print("Game Over");
                audio.PlayOneShot(gameOver);
                gameOverParticles.Play();
                state = GameState.Dying;
                Invoke("LoadNextScene", 1f);
                break;
        }
    }

    private void LoadNextScene()
    {
        if (state == GameState.Dying)
        {
            //gameOverParticles.Stop();
            SceneManager.LoadScene(0);
        }
        else if (state == GameState.Transition)
        {
            //clearLevelParticles.Stop();
            SceneManager.LoadScene(1);
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
        rigidBody.AddRelativeForce(Vector3.up * mainThrust);
        if (!audio.isPlaying)
        {
            audio.PlayOneShot(mainEngine);
        }
        mainEngineParticles.Play();
    }

    private void RespondToRotation()
    {
        rigidBody.freezeRotation = true;
        float rotationThisFrame = Time.deltaTime * rcsThrust;

        if ((Input.GetKey(KeyCode.A) == true) && (Input.GetKey(KeyCode.D) == false))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        if ((Input.GetKey(KeyCode.D) == true) && (Input.GetKey(KeyCode.A) == false))
        {
            transform.Rotate(Vector3.back * rotationThisFrame);
        }
        rigidBody.freezeRotation = false;
    }
}
