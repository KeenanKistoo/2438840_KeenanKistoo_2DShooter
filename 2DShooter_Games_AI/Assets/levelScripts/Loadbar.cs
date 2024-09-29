using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loadbar : MonoBehaviour
{
    public float maxTime;
    public float currentTime;
    public bool setTime;
    public GameObject bar;
    public bool startTimer;

    private RectTransform barRect;

    private void Start()
    {
        startTimer = false;
        barRect = bar.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (startTimer)
        {
            currentTime += Time.deltaTime;
            UpdateTime();
        }
    }

    public void StartLoad(float max)
    {
        SetMax(max);
        startTimer = true;
    }

    private void SetMax(float setMax)
    {
        if (!setTime)
        {
            maxTime = setMax;
            currentTime = 0;
            setTime = true;
            
        }
    }

    private void UpdateTime()
    {
        float timePer = currentTime / maxTime;

        Vector2 newWidth = barRect.sizeDelta;

        newWidth.x = timePer * 980f;

        barRect.sizeDelta = newWidth;

        if (currentTime >= maxTime)
        {
            startTimer = false;
        }
    }
}
