using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Vehicle", menuName = "Vehicle/Vehicle")]
public class VehicleStats : ScriptableObject
{
    public bool continuousInput;
    public float rotateSpeed;
    public float acceleration;
    public float maxVelocity;
    public float traction;
    public float backwardScalar;
    public float spinDragAngle;
    public float tiltAngle;
    public AudioClip driveSound;
}
