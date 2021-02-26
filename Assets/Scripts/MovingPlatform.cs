using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA, pointB;
    public bool pauseAtLocation;
    public float pauseLength = 0.5f;
    public float speed = 1f;

    private Transform target;
    private bool moving = true;

    void Start()
    {
        target = pointA;
    }

    void FixedUpdate()
    {
        if (!moving)
        {
            return;
        }

        if (IsAtTarget())
        {
            target = NextTarget();
            if (pauseAtLocation)
            {
                StartCoroutine("Pause");
            }
        }
        else
        {
            Move();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = this.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = null;
        }
    }


    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed);
    }

    private bool IsAtTarget()
    {
        if (target == null)
        {
            moving = false;
            return true;
        }
        return (transform.position - target.position).magnitude < 0.01f;
    }

    private Transform NextTarget()
    {
        if (target == pointA)
        {
            return pointB;
        }
        else
        {
            return pointA;
        }
    }

    IEnumerator Pause()
    {
        moving = false;

        yield return new WaitForSeconds(pauseLength);

        moving = true;
    }
}
