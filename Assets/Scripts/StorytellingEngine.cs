using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public static class StorytellingEngine {

    private static int levelNumber;
    private static int itemsToCollect;
    private static int itemsCollected = 0;
    
    public static void Initialize(int items, int level)
    {
        itemsToCollect = items;
        levelNumber = level;
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

        GameObject.Find("LevelManager").GetComponent<LevelManager>().FadeScreen(gameWon);
    }
}
