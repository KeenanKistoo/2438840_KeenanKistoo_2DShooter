using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBehaviour : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private GameObject _player;
    
    [Header("Animations:")]
    [SerializeField] private Animation _openAnim;
    [SerializeField] private Animation _idleAnim;

    [Header("Animator:")] 
    [SerializeField] private Animator _animator;
    

    private void Awake()
    {
        _player = GameObject.FindWithTag("Player");
        _animator = this.gameObject.GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject == _player)
        {
            _animator.SetTrigger("OpenChest");
        }

        if (coll.gameObject)
        {
            print("Collision");
        }
    }
}
