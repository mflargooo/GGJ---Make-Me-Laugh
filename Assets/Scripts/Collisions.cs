using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    [SerializeField] private LayerMask collisionLayers;
    [SerializeField] private int multiplier;
 
    [SerializeField] private PlayerController pc;

    private int basePts;
    private float playerSpeed;
    private void Start()
    {
        basePts = GameManager.basePts;

    }

    private void OnTriggerEnter(Collider other)
    {
        if ((collisionLayers.value & other.gameObject.layer << 2) > 0)
        {
            /* other code in killing enemy */
            GameManager.UpdateScore(multiplier * basePts);
            playerSpeed = pc.GetSpeed();
            other.gameObject.GetComponent<NPCController>().Hit(transform.root.forward * playerSpeed * .75f / Time.deltaTime + Vector3.up * 100f);
        }
    }
}
