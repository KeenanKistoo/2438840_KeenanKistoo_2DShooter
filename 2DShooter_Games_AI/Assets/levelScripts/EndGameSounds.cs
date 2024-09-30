using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameSounds : MonoBehaviour
{
    public AudioSource _src;
    public AudioClip _win;
    public AudioClip _lose;

    public bool win;
    public bool lose;

    public GameObject winPanel;
    public TMP_Text winText;

    private void Start()
    {
        win = false;
        lose = false;
        winPanel.SetActive(false);
    }

    private void Update()
    {
        if (win)
        {
            _src.clip = _win;
            _src.Play();
            winPanel.SetActive(true);
            winText.text = "You have found all the keys and escaped.";
            win = false;
        }else if (lose)
        {
            _src.clip = _lose;
            _src.Play();
            winPanel.SetActive(true);
            winText.text = "You died... The monster got you";
            lose = false;
        }
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
