using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

	public List<GameObject> inventory;
    private int lives = 3;
    public UnityEngine.UI.Image[] hearts = new UnityEngine.UI.Image[3];
    public Sprite damagedHeartImage;
    public GameObject damagePanel;
    private UnityEngine.UI.Image damageImage;
	
	// Use this for initialization
	void Start ()
	{
        StorytellingEngine.Initialize(3, SceneManager.GetActiveScene().name);
		inventory = new List<GameObject>();
        damageImage = damagePanel.GetComponent<UnityEngine.UI.Image>();
        damageImage.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

    public void Hit()
    {
        lives--;
        hearts[lives].sprite = damagedHeartImage;
        StartCoroutine(PlayerHurt());
        if (lives == 0) StorytellingEngine.GameOver(false);
    }

    private IEnumerator PlayerHurt()
    {
        damageImage.enabled = true;

        damageImage.color = new Color((229f / 255f), (25f / 255f), (25f / 255f), 0.55f);
        yield return new WaitForSeconds(0.15f);

        for (float i = 1f; i > 0f; i -= Time.deltaTime)
        {
            damageImage.color = new Color((229f / 255f), (25f / 255f), (25f / 255f), (i > 0.55f ? 0.55f : i));
            yield return null;
        }

        damageImage.enabled = false;
    }
}
