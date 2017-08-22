using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attached to each controller

[RequireComponent(typeof(Controller))]
public class Hand : MonoBehaviour {

    GameObject heldObject;
    Controller controller;

    Rigidbody simulator; // Enables the object to keep its velocity and trajectory once thrown

    public Valve.VR.EVRButtonId pickUpButton;
    public Valve.VR.EVRButtonId dropButton;
    

	// Use this for initialization
	void Start () {
        simulator = new GameObject().AddComponent<Rigidbody>();
        simulator.name = "simulator";
        simulator.transform.parent = transform.parent;

        controller = GetComponent<Controller>();
	}
	
	// Update is called once per frame
	void Update () {
        if (heldObject) // Check if there is a held object already in hand
        {
            simulator.velocity = (transform.position - simulator.position) * 50f; // Set the velocity for when the object is to be released
            if ((controller.controller.GetPressUp(pickUpButton) && heldObject.GetComponent<HeldObject>().dropOnRelease) || (controller.controller.GetPressDown(dropButton) && !heldObject.GetComponent<HeldObject>().dropOnRelease)) // Check if the grip is released
            {
                heldObject.transform.parent = null;
                heldObject.GetComponent<Rigidbody>().isKinematic = false;
                heldObject.GetComponent<Rigidbody>().velocity = simulator.velocity; // Give the object the velocity it had before it was released
                heldObject.GetComponent<HeldObject>().parent = null; // Allows the object to be picked up again
                heldObject = null;
            }
        }
        else
        {
            if (controller.controller.GetPressDown(pickUpButton)) // Check if the grip is being pressed
            {
                Collider[] cols = Physics.OverlapSphere(transform.position, 0.1f); // Create an array of colliders of everything that is within a small radius (0.1f) of the controller

                foreach (Collider col in cols) // Check each collider to see if they're ready to be a held object
                {
                    if (heldObject == null && col.GetComponent<HeldObject>() && col.GetComponent<HeldObject>().parent == null) // Checks if hand is empty, object is branded as a holdable object, and if that object has no parent
                    {
                        heldObject = col.gameObject;
                        heldObject.transform.parent = transform; // Sets the object's position to the controller - allows you to move it around
                        heldObject.transform.localPosition = Vector3.zero;
                        heldObject.transform.localRotation = Quaternion.identity;
                        heldObject.GetComponent<Rigidbody>().isKinematic = true;
                        heldObject.GetComponent<HeldObject>().parent = controller; // Sets the object's parent to the controller
                    }
                }
            }
        }
	}
}
