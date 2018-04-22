using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public Canvas blackCanvas;
    public UnityEngine.UI.Image panel;
    public GameObject restartButton;
    public GameObject levelButton;
    public UnityEngine.UI.Text gameOverText;
    public Canvas titleCanvas;
    public Canvas controlsCanvas;
    public Canvas creditsCanvas;

    private string thisLevelName;
    private int levelNumber;

    private bool levelWon;

    private bool storyTelling;

    private GameObject storyteller;
    private AudioSource storytellerWordOfMouth;

	// Use this for initialization
	void Start () {
        blackCanvas.enabled = false;
	    creditsCanvas.enabled = false;
	    controlsCanvas.enabled = false;

        storyTelling = false;
        storyteller = GameObject.Find("Storyteller");
        if (storyteller != null) storytellerWordOfMouth = storyteller.GetComponent<AudioSource>();

        thisLevelName = SceneManager.GetActiveScene().name;
        
        levelNumber = thisLevelName[thisLevelName.Length - 1];

        if (thisLevelName.Contains("campfire")) StorytellingEngine.TellStory(storyteller);
	}
	
	// Update is called once per frame
	void Update () {
        if(storyTelling)
        {
            if (!storytellerWordOfMouth.isPlaying) StartCoroutine(FadeThenLoad(1f));
        }
	}

    public void FadeScreen(bool gameWon, float alphaAsDecimal)
    {
        levelWon = gameWon;
        StartCoroutine(FadePanelIn(alphaAsDecimal));
    }

    public void LoadLevel(string levelName)
    {
        if(thisLevelName == "main")
        {
            SceneManager.LoadScene("level1");
        } else if (thisLevelName.Contains("campfire"))
        {
            SceneManager.LoadScene("level" + (levelNumber + 1));
        } else
        {
            if(levelWon)
            {
                if (levelNumber == 3)
                {
                    SceneManager.LoadScene("epilogue");
                }
                else SceneManager.LoadScene("campfire" + levelNumber);
            } else
            {
                if(levelName == "thisLevel")
                {
                    SceneManager.LoadScene(thisLevelName);
                } else
                {
                    SceneManager.LoadScene("main");
                }
            }
        }
    }

    public void StartGame()
    {
        StartCoroutine(FadeTitleOut());

        StorytellingEngine.TellStory(GameObject.Find("Storyteller"));
    }
    
    public void ShowControls()
    {
        titleCanvas.enabled = false;
        controlsCanvas.enabled = true;
    }
    
    public void ShowCredits()
    {
        titleCanvas.enabled = false;
        creditsCanvas.enabled = true;
    }

    public void HideControls()
    {
        controlsCanvas.enabled = false;
        titleCanvas.enabled = true;
    }
    
    public void HideCredits()
    {
        creditsCanvas.enabled = false;
        titleCanvas.enabled = true;
    }

    private IEnumerator FadePanelIn(float panelAlpha)
    {
        blackCanvas.enabled = true;
        panel.color = new Color(0f, 0f, 0f, 0f);

        try
        {
            restartButton.SetActive(false);
            levelButton.SetActive(false);

            gameOverText.text = (levelWon ? "Level complete!" : "You got caught by the Witch!");
            gameOverText.color = (levelWon ? new Color(0.615f, 1f, 0f) : new Color(0.94f, 0.415f, 0.415f));
        } catch (NullReferenceException) { }
        
        yield return null;
        
        for(float t = 0; t<1; t+= Time.deltaTime / 3f)
        {
            panel.color = new Color(0f, 0f, 0f, panelAlpha * t);
            yield return null;
        }

        panel.color = new Color(0f, 0f, 0f, panelAlpha);

        try
        {
            restartButton.SetActive(!levelWon);

            levelButton.SetActive(true);
            levelButton.GetComponentInChildren<UnityEngine.UI.Text>().text = (!levelWon || levelNumber == 3 ? "Main menu" : "Next level");
        } catch (NullReferenceException) { }

        yield return null;
    }

    private IEnumerator FadeThenLoad(float panelAlpha)
    {
        blackCanvas.enabled = true;
        panel.color = new Color(0f, 0f, 0f, 0f);

        yield return null;

        for (float t = 0; t < 1; t += Time.deltaTime / 3f)
        {
            panel.color = new Color(0f, 0f, 0f, panelAlpha * t);
            yield return null;
        }

        panel.color = new Color(0f, 0f, 0f, panelAlpha);

        yield return null;

        LoadLevel("");
    }

    private IEnumerator FadeTitleOut()
    {
        for(float t = 1; t > 0; t -= Time.deltaTime / 5f)
        {
            foreach (UnityEngine.UI.Image button in titleCanvas.GetComponentsInChildren<UnityEngine.UI.Image>()) button.color = new Color(button.color.r, button.color.g, button.color.b, t * button.color.a);
            foreach (UnityEngine.UI.Text text in titleCanvas.GetComponentsInChildren<UnityEngine.UI.Text>()) text.color = new Color(text.color.r, text.color.g, text.color.b, t * text.color.a);

            yield return null;
        }
        yield return null;

        titleCanvas.enabled = false;

        yield return null;
    }

    public void Storytelling()
    {
        storyTelling = true;
    }
}
