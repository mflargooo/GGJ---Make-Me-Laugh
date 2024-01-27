using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static int score;
    public static bool isGameOver = false;

    [SerializeField] private float gameTime;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text scoreText;

    [SerializeField] private GameObject angryChild;

    private GameObject currChild;

    private float timer;
    private void Start()
    {
        timer = gameTime + .99f;
    }
    private void Update()
    {
        if (isGameOver)
        {
            timerText.transform.gameObject.SetActive(false);
            scoreText.transform.gameObject.SetActive(false);
        }
        else
        {
            timerText.text = "Time left: " + (int)timer;
            scoreText.text = "Score: " + score.ToString();
            timer -= Time.deltaTime;
        }

        if(!currChild)
        {
            currChild = Instantiate(angryChild, new Vector3(Random.Range(-10f, 10f), 1f, Random.Range(-10f, 10f)), transform.rotation);
        }
    }
    public static void UpdateScore(int val)
    {
        score += val;
        print(score);
    }

    public static void GameOver()
    {
        print("Game Over");
        isGameOver = true;
        Time.timeScale = .5f;
    }
}
