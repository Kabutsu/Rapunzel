using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

	public List<GameObject> inventory;
	private Rigidbody rb;
	
	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		inventory = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

}
