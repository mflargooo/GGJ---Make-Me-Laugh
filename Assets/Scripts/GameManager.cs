using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int score;

    public static void UpdateScore(int val)
    {
        score += val;
        /* script to update canvas */
        print(score);
    }
}
