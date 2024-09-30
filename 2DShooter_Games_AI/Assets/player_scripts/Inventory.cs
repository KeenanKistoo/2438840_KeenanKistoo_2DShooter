using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [Header("Key Details:")]
    public int keyLvl;
    public int keyCollected;
    
    [Header("Wood Details:")]
    public int woodCount;

    [Header("Display Amounts:")] 
    public TMP_Text woodText;

    [Header("Key Count:")] 
    public GameObject[] keys;
    public TMP_Text keyfoundTxt;

    [Header("Endgame")] public EndGameSounds _endGame;

    private void Start()
    {
        keyCollected = 0;
        woodCount = 0;
        Color color = keyfoundTxt.color; // Get the current color
        color.a = 0f; // Modify the alpha value
        keyfoundTxt.color = color; // Assign the updated color back


        for (int i = 0; i < keys.Length; i++)
        {
            keys[i].transform.GetChild(1).GetComponent<Image>().color 
                = new Color32(88, 88, 88, 255);
        }
    }

    private void Update()
    {
        woodText.text = 'X' + woodCount.ToString();

        for (int i = 0; i < keys.Length; i++)
        {
            if (i < keyLvl)
            {
                keys[i].SetActive(true);
            }else if (i >= keyLvl)
            {
                keys[i].SetActive(false);
            }
        }

        if (keyCollected >= keyLvl)
        {
            _endGame.win = true;
        }
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
        keys[keyCollected].transform.GetChild(1).GetComponent<Image>().color 
            = Color.white;
        keyCollected += 1;
    }

    public void LevelReset()
    {
        keyCollected = 0;
        woodCount = 0;
    }

    public IEnumerator NotEnough()
    {
        woodText.color = Color.red;
        yield return new WaitForSeconds(1f);
        woodText.color = Color.white;
    }

    public IEnumerator KeyText()
    {
        Color color = keyfoundTxt.color; // Get the current color
        color.a = 255f; // Modify the alpha value
        keyfoundTxt.color = color; // Assign the updated color back
      
        yield return new WaitForSeconds(2f);
        Color color2 = keyfoundTxt.color; // Get the current color
        color.a = 0; // Modify the alpha value
        keyfoundTxt.color = color; // Assign the updated color back

    }
    
    
}
