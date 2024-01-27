using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Objective : MonoBehaviour
{
    [SerializeField] private Gradient countdownGradient;
    [SerializeField] private float countdownTime;

    [SerializeField] private GameObject targetPanel;
    [SerializeField] private TMP_Text targetPanelText;
    [SerializeField] private Image timerDial;

    [SerializeField] private GameObject target;
    private GameObject objective = null;

    private float timer;

    [SerializeField] private Health pH;

    public void Start()
    {
        StartCoroutine(DoObjectives());
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isGameOver)
        {
            targetPanel.SetActive(false);
            timerDial.gameObject.SetActive(false);
        }

        if (timer > 0 && objective)
        {
            timer -= Time.deltaTime;
            float timePercent = timer / countdownTime;
            timerDial.color = countdownGradient.Evaluate(timePercent);
            timerDial.fillAmount = timePercent;
        }
        else if (objective && pH)
        {
            pH.Damage();
            timerDial.gameObject.SetActive(false);
            StartCoroutine(DisplayPanel("Target got away.", 2f));
        }
        else if (timer > 0)
        {
            timer = 0f;
            timerDial.gameObject.SetActive(false);
            StartCoroutine(DisplayPanel("Target taken out!", 2f));
        }
    }

    private IEnumerator DisplayPanel(string msg, float time)
    {
        targetPanelText.text = msg;
        targetPanel.SetActive(true);
        yield return new WaitForSeconds(time);
        targetPanel.SetActive(false);
    }

    IEnumerator DoObjectives()
    {
        while (!GameManager.isGameOver)
        {
            yield return new WaitForSeconds(countdownTime + 15f);
            targetPanelText.text = "A new target has appeared!";
            targetPanel.SetActive(true);

            yield return new WaitForSeconds(2f);

            objective = Instantiate(target, new Vector3(Random.Range(-10f, 10f), 1f, Random.Range(-10f, 10f)), target.transform.rotation);

            targetPanel.SetActive(false);
            timerDial.gameObject.SetActive(true);
            timer = countdownTime;
        }
    }
}
