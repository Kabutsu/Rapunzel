using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

	public List<GameObject> inventory;
	private Rigidbody rb;
    private int lives = 3;
    public UnityEngine.UI.Image[] hearts = new UnityEngine.UI.Image[3];
    public Sprite damagedHeartImage;
	
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

    public void Hit()
    {
        lives--;
        hearts[lives].sprite = damagedHeartImage;
        if (lives == 0) GameOver();
    }

    private void GameOver()
    {
        GameObject.Find("Overlord").GetComponent<WizardFollow>().enabled = false;
        GetComponent<Raycast>().enabled = false;
        Debug.Log("Game Over");
    }

}
