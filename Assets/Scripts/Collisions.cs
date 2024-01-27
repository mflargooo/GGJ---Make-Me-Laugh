using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collisions : MonoBehaviour
{
    [SerializeField] private LayerMask collisionLayers;
    [SerializeField] private UnityEvent onCollide;

    private void OnTriggerEnter(Collider other)
    {
        if ((collisionLayers.value & other.gameObject.layer << 2) > 0)
        {
            onCollide?.Invoke();
            /* other code in killing enemy */
            Destroy(other.gameObject);
        }
    }
}
