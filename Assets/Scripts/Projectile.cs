using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 10f; // Урон от пули
    private bool hasCollided = false; // Флаг, чтобы избежать мгновенного уничтожения

    private void Start()
    {
        // Даем пуле кадр, чтобы выйти из коллайдера бота
        Invoke("EnableCollision", 0.05f);
    }
    private void EnableCollision()
    {
        hasCollided = true; // Разрешаем столкновения после небольшой задержки
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasCollided) return;
        if (other.CompareTag("Player")) // Убедись, что у игрока тег "Player"
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // Наносим урон
                Debug.Log("Игрок получил урон: " + damage);
            }
            Destroy(gameObject); // Уничтожаем пулю
        }
        else if (other.gameObject.layer != LayerMask.NameToLayer("Enemy")) // Не исчезает при столкновении с ботом
        {
            Destroy(gameObject); // Уничтожаем пулю при попадании в любой другой объект
        }
    }
}
