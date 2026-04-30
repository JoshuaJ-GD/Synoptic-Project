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

    [Header("Raycasting")]
    public float soundTimer = 1f;
    public AudioClip[] audioClips;
    private AudioSource audioSource;
    private float footstepTimer;
    private bool isMoving;
    private float lastStep;

    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
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

        MyInput();

        rb.linearDamping = drag;

        if (horizontalInput != 0 ||  verticalInput != 0)
            isMoving = true;
        else
            isMoving = false;

        if (isMoving && Time.time - lastStep >= soundTimer)
        {
            lastStep = Time.time;
            AudioClip clip = audioClips[Random.Range(0, audioClips.Length)];
            audioSource.PlayOneShot(clip);
            footstepTimer = soundTimer;
            GetComponent<AudioRayCasting>().CastRadialRays();
        }
        else
            footstepTimer = 0;
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
