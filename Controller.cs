using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attached to each controller

public class Controller : MonoBehaviour {

	public SteamVR_Controller.Device controller
    {
        get
        {
            // Retrieves controller information to avoid typing this out every time
            // Attached to both controllers
            return SteamVR_Controller.Input((int)GetComponent<SteamVR_TrackedObject>().index);
        }
    }
}
