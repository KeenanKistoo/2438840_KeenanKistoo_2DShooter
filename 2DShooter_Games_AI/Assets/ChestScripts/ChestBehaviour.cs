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

    [Header("Inventory Controller:")] 
    [SerializeField]
    private Inventory _inventory;
    

    private void Awake()
    {
        _player = GameObject.FindWithTag("Player");
        _animator = this.gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D coll)
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
