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
        Thrust();
        Rotation();
    }
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("Safe");
                break;
            default:
                print("Dead");
                break;
        }
    }
    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);
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
    private void Rotation()
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
