using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is added to everything you want to be holdable

[RequireComponent(typeof(Rigidbody))]
public class HeldObject : MonoBehaviour {

    // Tells the held object which input device it is being held by (ie which controller)
    [HideInInspector]
    public Controller parent;

    public bool dropOnRelease; // Variable that lets you choose whether you drop the object when you release the pickup button or drop it by pressing another button
}
