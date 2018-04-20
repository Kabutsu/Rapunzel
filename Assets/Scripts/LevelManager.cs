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
        if (levelName == "thisLevel")               //restart level
        {
            SceneManager.LoadScene(thisLevelName);
        } else if (levelName == "campfire1")
        {
            SceneManager.LoadScene("campfire1");    //start the game from main menu
        } else if (!levelWon)
        {
            SceneManager.LoadScene("main");         //quit to main menu
        } else
        {
            if(thisLevelName.Substring(0, 5) == "level")
            {
                if (levelNumber == 3)
                {
                    SceneManager.LoadScene("main"); //game finished; quit to main menu
                }
                else SceneManager.LoadScene("campfire" + (levelNumber + 1));    //load the next campfire scene
            }
            else SceneManager.LoadScene("level" + levelNumber);                 //load the next level
        }
    }

    private IEnumerator FadePanelIn()
    {
        gameOverCanvas.enabled = true;
        restartButton.SetActive(false);
        levelButton.SetActive(false);
        panel.color = new Color(0f, 0f, 0f, 0f);

        gameOverText.text = (levelWon ? "Level complete!" : "You got caught by the Witch!");
        gameOverText.color = (levelWon ? new Color(0.615f, 1f, 0f) : new Color(0.94f, 0.415f, 0.415f));

        yield return null;
        
        for(float t = 0; t<1; t+= Time.deltaTime / 3f)
        {
            panel.color = new Color(0f, 0f, 0f, 0.61f * t);
            yield return null;
        }

        panel.color = new Color(0f, 0f, 0f, 0.61f);

        restartButton.SetActive(!levelWon);

        levelButton.SetActive(true);
        levelButton.GetComponentInChildren<UnityEngine.UI.Text>().text = (!levelWon || levelNumber == 3 ? "Main menu" : "Next level");

        yield return null;
    }
}
