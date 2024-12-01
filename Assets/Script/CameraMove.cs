using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform playerTransform;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 playerPosition = playerTransform.position;
        playerPosition.z = transform.position.z;
        transform.position = playerPosition;
    }
}
