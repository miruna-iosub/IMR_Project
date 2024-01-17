using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MovingKinematic : XRGrabInteractable
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private bool isGrabbed = false;

    protected override void Awake()
    {
        base.Awake();

        // Store the initial position when the script starts
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        base.OnSelectEntered(interactor);

        // Set the flag to indicate that the object is grabbed
        isGrabbed = true;
    }

    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        base.OnSelectExited(interactor);

        // Return the object to its initial position when released
        if (isGrabbed)
        {
            transform.position = initialPosition;
            transform.rotation = initialRotation;
            isGrabbed = false;
        }
    }
}
