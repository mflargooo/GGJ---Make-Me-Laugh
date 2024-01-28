using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    [SerializeField] private LayerMask collisionLayers;
    [SerializeField] private int multiplier;
 
    [SerializeField] private PlayerController pc;
    Rigidbody pRb;
    Health pH;

    private float playerSpeed;
    private void Start()
    {
        pRb = pc.GetComponent<Rigidbody>();
        pH = pRb.GetComponent<Health>();
    }

    private void OnTriggerEnter(Collider other)
    {
        /* Hit Valid NPC */
        if (3 == other.gameObject.layer)
        {
            playerSpeed = pc.GetSpeed();
            other.gameObject.GetComponent<NPCController>().Hit(pRb.velocity.normalized * playerSpeed * .75f / Time.deltaTime + Vector3.up * 100f, multiplier);
        }
        /* Hit Obstacle */
        else if (!pH.invinc && 6 == other.gameObject.layer && pRb.velocity.magnitude > 5f)
        {
            StartCoroutine(pH.Slipped());
            if (other.gameObject.name.Contains("Banana")) Destroy(other.gameObject);
        }
        /* Hit Child */
        else if (!pH.invinc && 8 == other.gameObject.layer && pRb.velocity.magnitude > 5f)
        {
            pH.Damage();
            playerSpeed = pc.GetSpeed();
            other.gameObject.GetComponent<NPCController>().Hit(pRb.velocity.normalized * playerSpeed * .75f / Time.deltaTime + Vector3.up * 100f, 0);
        }
    }
}
