using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

	public List<GameObject> inventory;
    private int lives = 3;
    public UnityEngine.UI.Image[] hearts = new UnityEngine.UI.Image[3];
    public Sprite damagedHeartImage;
	
	// Use this for initialization
	void Start ()
	{
        StorytellingEngine.Initialize(3, 1);
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
        if (lives == 0) StorytellingEngine.GameOver(false);
    }

    private void GameOver()
    {
        
    }

}
