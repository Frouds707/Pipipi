using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 10f; // ���� �� ����
    private bool hasCollided = false; // ����, ����� �������� ����������� �����������

    private void Start()
    {
        // ���� ���� ����, ����� ����� �� ���������� ����
        Invoke("EnableCollision", 0.05f);
    }
    private void EnableCollision()
    {
        hasCollided = true; // ��������� ������������ ����� ��������� ��������
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasCollided) return;
        if (other.CompareTag("Player")) // �������, ��� � ������ ��� "Player"
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // ������� ����
                Debug.Log("����� ������� ����: " + damage);
            }
            Destroy(gameObject); // ���������� ����
        }
        else if (other.gameObject.layer != LayerMask.NameToLayer("Enemy")) // �� �������� ��� ������������ � �����
        {
            Destroy(gameObject); // ���������� ���� ��� ��������� � ����� ������ ������
        }
    }
}
