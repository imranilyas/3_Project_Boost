using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour
{
    public Transform player;
    public float max_distance = 5f;
    public float fall_speed = 2f;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 fallingObject = transform.position;
        if ((fallingObject.z - player.position.z) < max_distance)
        {
            rb.useGravity = true;
            rb.velocity = Vector3.up * Physics.gravity.y * fall_speed * Time.deltaTime;
        }
    }
}
