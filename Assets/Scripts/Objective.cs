using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objective : MonoBehaviour
{
    [SerializeField] private Gradient countdownGradient;
    [SerializeField] private float countdownTime;

    [SerializeField] private GameObject targetPanel;
    [SerializeField] private Image targetPanelImg;
    [SerializeField] private Image timerDial;
    [SerializeField] private Image timerImg;

    [SerializeField] private Sprite[] objectiveSprites;
    private float timer;

    public void Start()
    {
        StartCoroutine(DoObjectives());
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
            float timePercent = timer / countdownTime;
            timerDial.color = countdownGradient.Evaluate(timePercent);
            timerDial.fillAmount = timePercent;
        }
        else timerDial.gameObject.SetActive(false);
    }

    IEnumerator DoObjectives()
    {
        while (true)
        {
            yield return new WaitForSeconds(countdownTime + 15f);

            Sprite targetSprite = objectiveSprites[0];
            targetPanel.SetActive(true);
            targetPanelImg.sprite = targetSprite;

            yield return new WaitForSeconds(2f);

            targetPanel.SetActive(false);
            timerDial.gameObject.SetActive(true);
            timer = countdownTime;
            timerImg.sprite = targetSprite;
        }
    }
}
