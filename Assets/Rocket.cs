using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
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
        ProcessInput();
    }

    private void ProcessInput()
    {
        rigidBody.freezeRotation = true;
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up);
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
        if ((Input.GetKey(KeyCode.A) == true) && (Input.GetKey(KeyCode.D) == false))
        {
            transform.Rotate(Vector3.forward);
            print("Rotating Left");
        }
        if ((Input.GetKey(KeyCode.D) == true) && (Input.GetKey(KeyCode.A) == false))
        {
            transform.Rotate(Vector3.back);
            print("Rotating Right");
        }
        rigidBody.freezeRotation = false;
    }
}
