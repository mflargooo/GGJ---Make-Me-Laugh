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

    [SerializeField] private GameObject heartContainer;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] slipSounds;

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
        if (invincTimer > 0f)
        {
            invincTimer -= Time.deltaTime;
        }
        else if (invinc) invinc = false;
    }
    public void Damage()
    {
        invinc = true;
        invincTimer = invincTime;

        currHealth -= 1;
        Destroy(heartContainer.transform.GetChild(currHealth).gameObject);
        if (currHealth <= 0)
        {
            GameManager.GameOver();
            GameObject ragdoll = Instantiate(flop, transform.position + Vector3.up * .125f, transform.rotation);
            ragdoll.GetComponent<Rigidbody>().AddForce(GetComponent<Rigidbody>().velocity.normalized * 100f + Vector3.up * 40f, ForceMode.Impulse);
            Camera.main.transform.parent = ragdoll.transform;
            Destroy(gameObject);
        }
    }
    public IEnumerator Slipped()
    {
        audioSource.PlayOneShot(slipSounds[Random.Range(0, slipSounds.Length)]);

        Damage();
        recovering = true;
        float timer = recoverTime;
        
        while (timer >= 0)
        {
            timer -= Time.deltaTime;
            transform.Rotate(new Vector3(transform.eulerAngles.x, recoveringRotateSpeed * Time.deltaTime, transform.eulerAngles.z));
            yield return null;
        }

        recovering = false;
    }

    public int GetHealth()
    {
        return currHealth;
    }
}
