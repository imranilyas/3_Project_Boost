using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    Rigidbody rigidBody;
    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ThrusterInput();
        RotationInput();
    }

    private void ThrusterInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);
            print("Spacebar");
            if (!audio.isPlaying)
            {
                audio.Play(0);
            }
        }
        else
        {
            audio.Stop();
        }
    }
    private void RotationInput()
    {
        rigidBody.freezeRotation = true;
        float rotationThisFrame = Time.deltaTime * rcsThrust;

        if ((Input.GetKey(KeyCode.A) == true) && (Input.GetKey(KeyCode.D) == false))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
            print("Rotating Left");
        }
        if ((Input.GetKey(KeyCode.D) == true) && (Input.GetKey(KeyCode.A) == false))
        {
            transform.Rotate(Vector3.back * rotationThisFrame);
            print("Rotating Right");
        }
        rigidBody.freezeRotation = false;
    }
}
