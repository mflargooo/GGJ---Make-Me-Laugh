using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent nav;
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        StartCoroutine(Wander());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Wander()
    {
        while (true)
        {
            if ((nav.destination - transform.position).magnitude <= .5f)
            {
                nav.destination = transform.position;
                yield return new WaitForSeconds(Random.Range(1f, 2f));
                nav.destination = new Vector3(Random.Range(-10f, 10f), transform.position.y, Random.Range(-10f, 10f));
            }
            else
                yield return null;
        }
    }
}
