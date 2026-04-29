using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    [Header("Camera")]
    public float sensX;
    public float sensY;
    public Transform camOrient;
    float xRotate;
    float yRotate;

    [Header("Movement")]
    public float moveSpeed;
    public Transform playerOrient;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDir;
    Rigidbody rb;
    public float drag;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask ground;
    bool grounded;

    [Header("Raycasting")]
    public float soundTimer = 1f;

    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    public void Update()
    {
        // Mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotate += mouseX;
        xRotate -= mouseY;
        xRotate = Mathf.Clamp(xRotate, -90f, 90f);

        // Camera Rotation
        transform.rotation = Quaternion.Euler(0, yRotate, 0);
        camOrient.rotation = Quaternion.Euler(xRotate, yRotate, 0);

        // Ground Check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);

        MyInput();

        if (grounded)
            rb.linearDamping = drag;
        else
            rb.linearDamping = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        moveDir = playerOrient.forward * verticalInput + playerOrient.right * horizontalInput;

        rb.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);
    }
}
