using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Health hs;
    private AudioSource audioSource;

    private Vector2 input = Vector2.zero;
    private float moveSpeed = 0f;

    private float dragVelocity = 0f;
    private float tiltVelocity = 0f;
    private float dragAngle = 0f;
    private float tiltAngle = 0f;
    private float deadlyVelocityThreshold = 0f;
    private float fovVelocity = 0f;

    [SerializeField] private VehicleStats vs;
    [SerializeField] private GameObject model;
    [SerializeField] private GameObject frontAttack;
    [SerializeField] private float minSwitchTime;
    [SerializeField] private float maxSwitchTime;

    [SerializeField] private VehicleStats[] vehicles;
    [SerializeField] private GameObject discreteMovementTooltip;
    private int presses = 0;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hs = GetComponent<Health>();
        audioSource = GetComponent<AudioSource>();

        StartCoroutine(RandomSwitch());
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.isGameOver)
        {
            discreteMovementTooltip.SetActive(false);
            return;
        }

        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        deadlyVelocityThreshold = vs.maxVelocity * .75f;

        if (hs.recovering) return;

        if (vs.continuousInput)
            ContinuousMovement();
        else
            DiscreteMovement();
        RotationDrag();
        AttackHandler();

        Camera.main.fieldOfView = Mathf.SmoothDamp(Camera.main.fieldOfView, Mathf.Clamp(9.5f * rb.velocity.magnitude, 60f, 70f), ref fovVelocity, .2f);

        Audio();
    }

    void ContinuousMovement()
    {
        if (input.y == 0)
        {
            if (rb.velocity.magnitude > .05f)
                moveSpeed *= vs.traction;
            else
                rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
        }
        else
            moveSpeed = Mathf.Clamp(moveSpeed + vs.acceleration * input.y * Time.deltaTime * Time.deltaTime, -vs.maxVelocity * vs.backwardScalar, vs.maxVelocity);

        rb.velocity = Vector3.up * rb.velocity.y + transform.forward * moveSpeed;
        transform.Rotate(Vector2.up * input.x * input.y * (rb.velocity.magnitude > .1f ? 1f : 0f) * vs.rotateSpeed * Time.deltaTime);

        tiltAngle = Mathf.SmoothDampAngle(model.transform.eulerAngles.x, input.y * vs.tiltAngle, ref tiltVelocity, .2f);
        model.transform.eulerAngles = new Vector3(tiltAngle, model.transform.eulerAngles.y, model.transform.eulerAngles.z);
    }

    void DiscreteMovement()
    {
        if (rb.velocity.magnitude > .05f)
            moveSpeed *= vs.traction;
        else
            moveSpeed = 0f;

        if (Input.GetKeyDown(KeyCode.W))
        {
            presses += 1;
            moveSpeed = Mathf.Clamp(moveSpeed + vs.acceleration * Time.deltaTime * Time.deltaTime, -vs.maxVelocity * vs.backwardScalar, vs.maxVelocity);
        }

        rb.velocity = Vector3.ClampMagnitude(Vector3.up * rb.velocity.y + transform.forward * moveSpeed, vs.maxVelocity);

        rb.transform.Rotate(Vector2.up * input.x * (rb.velocity.magnitude > .1f ? 1f : 0f) * vs.rotateSpeed * Time.deltaTime);
        tiltAngle = Mathf.SmoothDampAngle(model.transform.eulerAngles.x, vs.tiltAngle, ref tiltVelocity, .2f);
        model.transform.eulerAngles = new Vector3(tiltAngle, model.transform.eulerAngles.y, model.transform.eulerAngles.z);
    }

    void RotationDrag()
    {
        dragAngle = Mathf.SmoothDampAngle(Camera.main.transform.eulerAngles.z, -input.x * (vs.continuousInput ? input.y : (rb.velocity.magnitude > .1f ? 1 : 0)) * vs.spinDragAngle * (input.y < 0 ? vs.backwardScalar : 1f), ref dragVelocity, .2f);
        Camera.main.transform.eulerAngles = new Vector3(Camera.main.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, dragAngle);
    }

    void AttackHandler()
    {
        frontAttack.SetActive(rb.velocity.magnitude > deadlyVelocityThreshold && Vector3.Dot(rb.velocity, transform.forward) > 0);
        //backAttack.SetActive(rb.velocity.magnitude > deadlyVelocityThreshold * vs.backwardScalar && Vector3.Dot(rb.velocity, transform.forward) < 0);
    }

    IEnumerator RandomSwitch()
    {
        while (true)
        {
            vs = vehicles[Random.Range(0, vehicles.Length)];
            if(!vs.continuousInput)
            {
                presses = 0;
                StartCoroutine(Flash(discreteMovementTooltip));
            }
            print("Switched to " + vs.name + "!");
            yield return new WaitForSeconds(Random.Range(minSwitchTime, maxSwitchTime));
        }
    }

    IEnumerator Flash(GameObject obj)
    {
        while (presses < 10 && !vs.continuousInput)
        {
            obj.SetActive(true);
            yield return new WaitForSeconds(.25f);
            obj.SetActive(false);
            yield return new WaitForSeconds(.25f);
        }
    }

    void Audio()
    {
        if (vs.driveSound)
        {
            audioSource.clip = vs.driveSound;
            audioSource.Play();
            audioSource.volume = rb.velocity.magnitude / vs.maxVelocity;
        }
    }

    public float GetSpeed()
    {
        return moveSpeed;
    }
}
