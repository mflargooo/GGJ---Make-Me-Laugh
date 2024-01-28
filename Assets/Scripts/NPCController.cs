using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent nav;
    [SerializeField] private GameObject flop;
    [SerializeField] private int points;

    [SerializeField] private PlayerController player;
    [SerializeField] private float wanderRadius;

    bool resetChase = false;
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindObjectOfType<PlayerController>();
        nav.destination = transform.position;
        if (name.Contains("NPC")) StartCoroutine(Wander());
        else if (name.Contains("Child"))
        {
            if (player)
            {
                nav.destination = player.transform.position;
                StartCoroutine(Chase());
            }
            else StartCoroutine(Wander());
        }
        else if (name.Contains("Criminal"))
        {
            if (player)
            {
                StartCoroutine(Avoid());
            }
            else
                StartCoroutine(Wander());
        }
    }

    Vector3 RandomPosition()
    {
        NavMeshHit hit;
        Vector3 randDir = Random.insideUnitSphere * 10f + transform.position;
        NavMesh.SamplePosition(randDir, out hit, 10f, 1);
        return hit.position;
    }
    public void ResetChase()
    {
        resetChase = true;
        nav.destination = RandomPosition();
    }

    private IEnumerator Avoid()
    {
        while (player)
        {
            nav.destination = transform.position + (new Vector3(transform.position.x, 0f, transform.position.z) - new Vector3(player.transform.position.x, 0f, player.transform.position.z)).normalized * 5f;
            yield return null;
        }

        StartCoroutine(Wander());
    }

    IEnumerator Chase()
    {
        while (player)
        {
            if (!player.GetComponent<Health>().invinc && (transform.position - player.transform.position).magnitude < 1.5f && player.GetComponent<Rigidbody>().velocity.magnitude < 5f)
            {
                player.GetComponent<Health>().Damage();
                ResetChase();
            }

            if (!resetChase)
                nav.destination = player.transform.position;
            else if ((nav.destination - transform.position).magnitude < .1f) resetChase = false;
            yield return null;
        }

        StartCoroutine(Wander());
    }
    IEnumerator Wander()
    {

        while (true)
        {
            if ((nav.destination - transform.position).magnitude <= .5f)
            {
                nav.destination = transform.position;
                yield return new WaitForSeconds(Random.Range(1f, 2f));
                nav.destination = RandomPosition();

            }
            else
                yield return null;
        }
    }

    public void Hit(Vector3 force, int ptsMultiplier)
    {
        GameObject ragdoll = Instantiate(flop, transform.position + Vector3.up, transform.rotation);
        ragdoll.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        GameManager.UpdateScore(points * ptsMultiplier);
        Destroy(gameObject);
    }
}
