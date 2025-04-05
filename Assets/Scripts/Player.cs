using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Character
{
    [SerializeField] private PlayerController controller;
    [SerializeField] private WeaponManager weaponManager;
    private bool isDead = false;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        OnDeath += HandleDeath;
    }

    protected override void Start()
    {
        base.Start();

        if (agent != null)
        {
            agent.enabled = false;
        }

        controller = GetComponent<PlayerController>();
        weaponManager = GetComponent<WeaponManager>();
    }

    private void Update()
    {
        if (isDead) return;

        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.C))
        {
            ToggleCursor(Cursor.lockState == CursorLockMode.Locked);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponManager.SwitchWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weaponManager.SwitchWeapon(1);
        }

        Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        controller.Rotate(mouseInput);

        if (Input.GetMouseButton(0))
        {
            weaponManager.Shoot(controller.GetLookDirection());
        }
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(moveInput);
    }

    private void ToggleCursor(bool visible)
    {
        Cursor.lockState = visible ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = visible;
    }

    protected override void Die()
    {
        base.Die();
    }

    private void HandleDeath()
    {
        isDead = true;
        controller.DisableMovement();
        weaponManager.DisableWeapon();
        ToggleCursor(true);
        GameManager.instance.ShowDeathMenu();
    }

    public void ResetPlayer()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        currentHealth = maxHealth;
        isDead = false;
        controller.EnableMovement();
        weaponManager.EnableWeapon();
        ToggleCursor(false);
        GameManager.instance.HideDeathMenu();
    }
}   
