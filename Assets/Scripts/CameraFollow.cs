using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    enum Axis { x, y, z, xy, xz, yz };
    [SerializeField] Axis direction;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        if (direction == Axis.x) { FollowX(); }
        if (direction == Axis.y) { FollowY(); }
        if (direction == Axis.z) { FollowZ(); }
        if (direction == Axis.xy) { FollowX(); FollowY(); }
        if (direction == Axis.xz) { FollowX(); FollowZ(); }
        if (direction == Axis.yz) { FollowY(); FollowZ(); }

    }

    private void FollowX()
    {
        Vector3 position = transform.position;
        position.x = target.position.x + offset.x;
        transform.position = position;        
    }

    private void FollowZ()
    {
        Vector3 position = transform.position;
        position.z = target.position.z + offset.z;
        transform.position = position;
    }

    private void FollowY()
    {
        Vector3 position = transform.position;
        position.y = target.position.y + offset.y;
        transform.position = position;
    }
}
