using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {
    public Brain playerBrain,emptyBrain;
	// Use this for initialization
	void Start () {
        if (isLocalPlayer)
        {
            GetComponent<Motor>().brain = playerBrain;
            GetComponent<Motor>().gun.Initialize(GetComponent<Motor>());
        }
        else
        {
            GetComponent<Motor>().brain = emptyBrain;
            GetComponent<Motor>().gun.Initialize(GetComponent<Motor>());
        }
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
