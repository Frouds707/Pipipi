using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private Camera playerCamera;
    private Rigidbody rb;
    private float xRotation = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null) rb = gameObject.AddComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = true;

        if (playerCamera == null) playerCamera = Camera.main;
    }

    public void Move(Vector3 input)
    {
        Vector3 movement = input.normalized * moveSpeed;
        Vector3 velocity = transform.TransformDirection(movement);
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;
    }

    public void Rotate(Vector2 mouseInput)
    {
        float mouseX = mouseInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = mouseInput.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    public Vector3 GetLookDirection()
    {
        return playerCamera.transform.forward;
    }
}