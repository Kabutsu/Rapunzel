using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (gameWon)
        {
            GameWin();
        }
        else GameLose();
    }

    private static void GameWin()
    {
        Debug.Log("You Win");
    }

    private static void GameLose()
    {

        GameObject.Find("Overlord").GetComponent<WizardFollow>().enabled = false;
        GameObject.Find("FPSController").GetComponent<Raycast>().enabled = false;
        Debug.Log("Game Over");
    }
}
