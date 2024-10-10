﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRRig : MonoBehaviour
{
    public float turnSmoothness;
    public VRMap head;
    public VRMap leftHand;
    public VRMap rightHand;
    public Transform headConstraint;
    public Vector3 headBodyOffset;

    private void Start()
    {
        headBodyOffset = transform.position - headConstraint.position;
    }

    private void LateUpdate()
    {
        transform.position = headConstraint.position + headBodyOffset;
        transform.forward = Vector3.Lerp(transform.forward,Vector3.ProjectOnPlane(headConstraint.up,Vector3.up).normalized,Time.deltaTime * turnSmoothness);

        head.Map();
        leftHand.Map();
        rightHand.Map();
    }
}

[System.Serializable]
public class VRMap
{
    public Transform vrTarget;
    public Transform rigTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;

    public void Map()
    {
        rigTarget.position = vrTarget.InverseTransformPoint(trackingPositionOffset);
        rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}
