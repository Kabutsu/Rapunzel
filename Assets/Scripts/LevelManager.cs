using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public Canvas gameOverCanvas;
    public UnityEngine.UI.Image panel;
    public GameObject restartButton;
    public GameObject levelButton;
    public UnityEngine.UI.Text gameOverText;

    private string thisLevelName;
    private int levelNumber;

    private bool levelWon;

	// Use this for initialization
	void Start () {
        gameOverCanvas.enabled = false;
        thisLevelName = SceneManager.GetActiveScene().name;

        levelNumber = thisLevelName[thisLevelName.Length - 1];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void FadeScreen(bool gameWon)
    {
        levelWon = gameWon;
        StartCoroutine(FadePanelIn());
    }

    public void LoadLevel(string levelName)
    {
        if(levelName == "thisLevel")
        {
            SceneManager.LoadScene(thisLevelName);
        } else if (levelNumber == 3 || !levelWon)
        {
            SceneManager.LoadScene("main");
        } else
        {
            SceneManager.LoadScene("campfire" + (levelNumber + 1));
        }
    }

    private IEnumerator FadePanelIn()
    {
        gameOverCanvas.enabled = true;
        restartButton.SetActive(false);
        levelButton.SetActive(false);
        panel.color = new Color(0f, 0f, 0f, 0f);

        gameOverText.text = (levelWon ? "Level complete!" : "You got caught by the witch!");

        yield return null;
        
        for(float t = 0; t<1; t+= Time.deltaTime / 3f)
        {
            panel.color = new Color(0f, 0f, 0f, 0.61f * t);
            yield return null;
        }

        panel.color = new Color(0f, 0f, 0f, 0.61f);

        restartButton.SetActive(!levelWon);

        levelButton.SetActive(true);
        levelButton.GetComponentInChildren<UnityEngine.UI.Text>().text = (levelWon ? "Next level" : "Main menu");

        yield return null;
    }
}
