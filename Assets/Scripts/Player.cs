using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 6f;
    [SerializeField]
    private float gravity = 1f;
    [SerializeField]
    private float jumpHeight = 15f;
    private CharacterController controller;
    private float yVelocity;
    private int maxJumps = 2;
    private int jumps = 0;
    private UIManager UI;
    private Vector3 spawnPoint = new Vector3(3.96f, 5.28f, 0);
    private MeshRenderer rend;
    private bool useGravity = true;
    private bool isDead = false;
    private float spawnDelay = 0.25f;


    void Start()
    {
        Initialize();
    }

    void Update()
    {
        if (!isDead)
        {
            Move();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DeadZone" && !isDead)
        {
            Die();
        }
    }


    public void Die()
    {
        isDead = true;
        useGravity = false;
        transform.position = spawnPoint;
        UI.UpdateLives(-1);

        rend.enabled = false;
        if (UI.lives >= 0)
        {
            StartCoroutine("Respawn");
        }
        else
        {
            Destroy(this);
        }
    }


    private void Initialize()
    {
        controller = GetComponent<CharacterController>();
        rend = GetComponent<MeshRenderer>();
        UI = GameObject.Find("UI").GetComponent<UIManager>();
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(horizontalInput, 0f, 0f);
        Vector3 velocity = direction * speed;

        if (controller.isGrounded)
        {
            jumps = 0;

            if (IsJumpPressed())
            {
                Jump();
            }
        }
        else
        {
            if (IsJumpPressed() && jumps < maxJumps)
            {
                Jump();
            }
            if (useGravity)
            {
                yVelocity -= gravity;
            }
        }

        velocity.y = yVelocity;

        controller.Move(velocity * Time.deltaTime);
    }

    private bool IsJumpPressed()
    {
        return (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 0"));
    }

    private void Jump()
    {
        jumps++;
        yVelocity = jumpHeight;
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(spawnDelay);

        rend.enabled = true;
        useGravity = true;
        isDead = false;
    }
}
