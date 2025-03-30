using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private PlayerController controller;
    [SerializeField] private WeaponManager weaponManager;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    protected override void Start()
    {
        base.Start();
        agent.enabled = false;
        controller = GetComponent<PlayerController>();
        weaponManager = GetComponent<WeaponManager>();
        if (controller == null) Debug.LogError("PlayerController не найден!");
        if (weaponManager == null) Debug.LogError("WeaponManager не найден!");
    }

    private void Update()
    {
        Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        controller.Rotate(mouseInput);

        if (Input.GetMouseButton(0))
        {
            weaponManager.Shoot(controller.GetLookDirection());
        }

        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.C))
        {
            ToggleCursor(Cursor.lockState == CursorLockMode.Locked);
            Debug.Log("Переключение курсора: " + (Cursor.visible ? "Виден" : "Скрыт"));
        }

        // Переключение оружия
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponManager.SwitchWeapon(0); // Пистолет
            Debug.Log("Переключено на пистолет");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weaponManager.SwitchWeapon(1); // Ракетница
            Debug.Log("Переключено на ракетницу");
        }
    }

    private void FixedUpdate()
    {
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(moveInput);
    }

    private void ToggleCursor(bool visible)
    {
        Cursor.lockState = visible ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = visible;
    }
}