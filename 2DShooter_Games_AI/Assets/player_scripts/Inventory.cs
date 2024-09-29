using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    [Header("Key Details:")]
    public int keyLvl;
    public int keyCollected;
    
    [Header("Wood Details:")]
    public int woodCount;

    [Header("Display Amounts:")] 
    public TMP_Text woodText;

    private void Start()
    {
        keyCollected = 0;
        woodCount = 0;
    }

    private void Update()
    {
        woodText.text = 'X' + woodCount.ToString();
    }

    public void CollectWood(int addWood)
    {
        woodCount += addWood;
    }

    public void UseWood(int subWood)
    {
        woodCount -= subWood;
    }

    public void FoundKey()
    {
        keyCollected += 1;
    }

    public void LevelReset()
    {
        keyCollected = 0;
        woodCount = 0;
    }
    
    
}
