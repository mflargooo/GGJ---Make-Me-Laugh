using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private float recoverTime;
    [SerializeField] private float recoveringRotateSpeed;
    [SerializeField] private float invincTime;
    [SerializeField] private GameObject flop;

    private int currHealth;

    public bool recovering { get; private set; }
    public bool invinc { get; private set; }

    private float invincTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        recovering = false;
        invinc = false;
        currHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (invincTime > 0f)
        {
            invincTime -= Time.deltaTime;
        }
        else if (invinc) invinc = false;
    }

    public IEnumerator Hit()
    {
        invinc = true;
        currHealth -= 1;
        recovering = true;
        float timer = recoverTime;
        if (currHealth <= 0)
        {
            GameManager.GameOver();
            GameObject ragdoll = Instantiate(flop, transform.position + Vector3.up * .125f, transform.rotation);
            ragdoll.GetComponent<Rigidbody>().AddForce(GetComponent<Rigidbody>().velocity.normalized * 100f + Vector3.up * 40f, ForceMode.Impulse);
            Camera.main.transform.parent = ragdoll.transform;
            Destroy(gameObject);
        }
        
        while (timer >= 0)
        {
            timer -= Time.deltaTime;
            transform.Rotate(new Vector3(transform.eulerAngles.x, recoveringRotateSpeed * Time.deltaTime, transform.eulerAngles.z));
            yield return null;
        }

        recovering = false;
        invincTimer = invincTime;
    }

    public int GetHealth()
    {
        return currHealth;
    }
}
