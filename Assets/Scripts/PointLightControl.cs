using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]

public class PointLightControl : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Light>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) gameObject.GetComponent<Light>().enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) gameObject.GetComponent<Light>().enabled = false;
    }
}
