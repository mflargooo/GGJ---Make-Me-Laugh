using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
        Time.timeScale = 1f;

        if (scene != 3 /*lose*/)
        {
            GameManager.ResetScore();
            GameManager.isGameOver = false;
        }
    }

    public void LoadSceneAfterTime (int scene, float time)
    {
        StartCoroutine(TimedLoad(scene, time));
    }

    IEnumerator TimedLoad(int scene, float time)
    {
        yield return new WaitForSeconds(time);
        if (scene != 3 /*lose*/) GameManager.isGameOver = false;
        Time.timeScale = 1f;
        GameManager.ResetScore();
        SceneManager.LoadScene(scene);
    }
}
