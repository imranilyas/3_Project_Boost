using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        position.y = target.position.y + offset.y;
        transform.position = position;
    }
}
