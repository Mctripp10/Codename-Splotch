using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timeValue = 90f;
    public TextMeshProUGUI timerText;
    public bool gameStart = true;

    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (timeValue > 0) {
            timeValue -= Time.deltaTime;
        } else {
            timeValue = 0;
        }

        if (gameStart) DisplayTime(timeValue);
    }

    void DisplayTime (float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    
    }
}
