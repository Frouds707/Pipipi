using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] private float respawnTime = 15f; 
    protected Vector3 initialPosition; 
    protected Quaternion initialRotation; 
    private float timer;
    private bool isPickedUp = false;

    protected virtual void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    private void Update()
    {
        if (isPickedUp)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Respawn();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPickedUp)
        {
            PlayerPickupHandler handler = other.GetComponent<PlayerPickupHandler>();
            if (handler != null && ApplyEffect(handler))
            {
                StartRespawn();
                Debug.Log($"{GetType().Name} подобран!");
            }
        }
    }

    private void StartRespawn()
    {
        isPickedUp = true;
        timer = respawnTime;
        gameObject.SetActive(false);
    }

    private void Respawn()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        gameObject.SetActive(true);
        isPickedUp = false;
        Debug.Log($"{GetType().Name} возродился в: {initialPosition}");
    }
    protected abstract bool ApplyEffect(PlayerPickupHandler handler);
}