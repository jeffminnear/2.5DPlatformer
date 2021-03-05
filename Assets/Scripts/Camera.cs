using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private float minY = -8f;

    void Update()
    {
        if (transform.position.y < minY)
        {
            Vector3 position = new Vector3(transform.position.x, minY, transform.position.z);
            transform.position = position;
        }
    }
}
