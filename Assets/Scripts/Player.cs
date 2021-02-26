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
    private UIManager ui;


    void Start()
    {
        Initialize();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
        Move();
    }


    private void Initialize()
    {
        controller = GetComponent<CharacterController>();
        ui = GameObject.Find("UI").GetComponent<UIManager>();
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
            yVelocity -= gravity;
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
}
