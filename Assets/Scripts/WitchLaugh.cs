using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchLaugh : MonoBehaviour {

    private const int MINIMUM_TIME_BETWEEN_LAUGHS = 10;
    private const int MAXIMUM_TIME_BETWEEN_LAUGHS = 20;
    private float timePassed = 0f;
    private int timeToWait = 0;

    public AudioClip witchLaughSound;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        timePassed += Time.deltaTime;
	}

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player") && timePassed >= timeToWait)
        {
            GetComponent<AudioSource>().PlayOneShot(witchLaughSound);
            timeToWait = Random.Range(MINIMUM_TIME_BETWEEN_LAUGHS, MAXIMUM_TIME_BETWEEN_LAUGHS);
            timePassed = 0f;
        }
    }
}
