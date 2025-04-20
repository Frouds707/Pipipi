using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class Pickup : MonoBehaviour
{
    [SerializeField] private float respawnTime = 10f;

    protected Vector3 initialPosition;
    protected Quaternion initialRotation;

    private Collider pickupCollider;
    private Renderer[] renderers;

    protected virtual void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        pickupCollider = GetComponent<Collider>();
        renderers = GetComponentsInChildren<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPickupHandler handler = other.GetComponent<PlayerPickupHandler>();
            if (handler != null && ApplyEffect(handler))
            {
                StartRespawn();
            }
        }
    }

    private void StartRespawn()
    {
        SetPickupActive(false);
        StartCoroutine(RespawnAfterDelay(respawnTime));
    }

    private IEnumerator RespawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Respawn();
    }

    private void Respawn()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        SetPickupActive(true);
    }

    private void SetPickupActive(bool value)
    {
        if (pickupCollider != null)
            pickupCollider.enabled = value;

        foreach (Renderer r in renderers)
            r.enabled = value;
    }

    protected abstract bool ApplyEffect(PlayerPickupHandler handler);
}