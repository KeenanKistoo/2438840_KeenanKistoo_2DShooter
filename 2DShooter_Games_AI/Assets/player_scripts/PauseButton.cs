using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public GameObject pausePanel;
    public PlayerMovement _player;

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        _player.playerReady = false;
    }

    public void ReturnToGame()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        _player.playerReady = true;
        pausePanel.SetActive(false);
    }

}
