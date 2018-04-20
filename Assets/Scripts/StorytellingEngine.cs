using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public static class StorytellingEngine {

    private static string sceneName;
    private static int itemsToCollect;
    private static int itemsCollected = 0;
    
    public static void Initialize(int items, string scene)
    {
        itemsToCollect = items;
        sceneName = scene;
    }

    public static void ItemCollected()
    {
        itemsCollected++;
        if (itemsCollected == itemsToCollect)
        {
            GameOver(true);
        }
    }

    public static void GameOver(bool gameWon)
    {
        GameObject.Find("FPSController").GetComponent<FirstPersonController>().enabled = false;
        GameObject.Find("FPSController").GetComponent<Raycast>().enabled = false;
        GameObject.Find("Overlord").GetComponent<WizardFollow>().enabled = false;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        GameObject.Find("LevelManager").GetComponent<LevelManager>().FadeScreen(gameWon, 0.61f);
    }

    public static void TellStory(GameObject storyteller)
    {
        AudioSource wordOfMouth = storyteller.GetComponent<AudioSource>();
        wordOfMouth.PlayOneShot(wordOfMouth.clip);

        GameObject.Find("LevelManager").GetComponent<LevelManager>().Storytelling();
    }
}
