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
    private int basePts;
    private float playerSpeed;
    private void Start()
    {
        basePts = GameManager.basePts;
        pRb = pc.GetComponent<Rigidbody>();
        pH = pRb.GetComponent<Health>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (3 == other.gameObject.layer)
        {
            GameManager.UpdateScore(multiplier * basePts);
            playerSpeed = pc.GetSpeed();
            other.gameObject.GetComponent<NPCController>().Hit(pRb.velocity.normalized * playerSpeed * .75f / Time.deltaTime + Vector3.up * 100f);
        }
        else if (!pH.invinc && 6 == other.gameObject.layer && pRb.velocity.magnitude > 5f)
        {
            StartCoroutine(pH.Hit());
        }
    }
}
