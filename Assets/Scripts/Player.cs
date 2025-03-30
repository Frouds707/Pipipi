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
        if (controller == null) Debug.LogError("PlayerController �� ������!");
        if (weaponManager == null) Debug.LogError("WeaponManager �� ������!");
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
            Debug.Log("������������ �������: " + (Cursor.visible ? "�����" : "�����"));
        }

        // ������������ ������
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponManager.SwitchWeapon(0); // ��������
            Debug.Log("����������� �� ��������");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weaponManager.SwitchWeapon(1); // ���������
            Debug.Log("����������� �� ���������");
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