using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    
    public void Reset()
    {
        //print("Is working");
        _player = GameObject.FindGameObjectWithTag("Player");

        _player.transform.position = new Vector3(0, 0, 0);
        
    }

    public void LeaveGame()
    {
        Application.Quit();
    }
}
