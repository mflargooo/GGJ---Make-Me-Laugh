using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static int score;
    public static bool isGameOver = false;

    [SerializeField] private Vector3 roomCenter;
    [SerializeField] private Vector3 roomRectangle;
    [SerializeField] private float gameTime;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text scoreText;

    [SerializeField] private GameObject angryChild;
    [SerializeField] private GameObject[] people;
    [SerializeField] private uint maxChildren;
    [SerializeField] private uint maxPeople;

    private GameObject[] currChilds;
    private GameObject[] currPeople;

    private float timer;
    private void Start()
    {
        timer = gameTime + .99f;
        currChilds = new GameObject[maxChildren];
        currPeople = new GameObject[maxPeople];
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

        for (int i = 0; i < maxChildren; i++)
            if (!currChilds[i])
                currChilds[i] = Instantiate(angryChild, new Vector3(Random.Range(-roomRectangle.x, roomRectangle.x), 0f, Random.Range(-roomRectangle.y, roomRectangle.y)) + roomCenter, transform.rotation);

        for (int i = 0; i < maxPeople; i++)
            if (!currPeople[i])
                currPeople[i] = Instantiate(people[Random.Range(0, people.Length)], new Vector3(Random.Range(-roomRectangle.x, roomRectangle.x), 0f, Random.Range(-roomRectangle.y, roomRectangle.y)) + roomCenter, transform.rotation);
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
