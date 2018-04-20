using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlicker : MonoBehaviour {

    public float minIntensity = 3;
    public float maxIntensity = 6;
    public float frequency = 3;

    private Light fire;
    private float randomX;

	// Use this for initialization
	void Start () {
        fire = GetComponent<Light>();
        randomX = Random.Range(1f, 100f);
	}
	
	// Update is called once per frame
	void Update () {
        float noise = Mathf.PerlinNoise(randomX, frequency * Time.time);
        fire.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
	}
}
