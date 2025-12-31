using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject hook;
    void Update()
    {
        if (hook != null)
        {
            Vector3 newPosition = hook.transform.position;
            newPosition.x = transform.position.x;
            newPosition.z = -10; // Set the z position of the camera
            transform.position = newPosition;
        }
    }
}