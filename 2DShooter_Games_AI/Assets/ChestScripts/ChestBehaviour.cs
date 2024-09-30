using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public bool key;

    [Header("Communication")] 
    [SerializeField] private GameObject addWood;
    [SerializeField] private GameObject subWood;
    

    private void Awake()
    {
        _player = GameObject.FindWithTag("Player");
        _animator = this.gameObject.GetComponent<Animator>();
        _inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject == _player)
        {
            _animator.SetTrigger("OpenChest");

            if (key)
            {
                _inventory.FoundKey();
                StartCoroutine(_inventory.KeyText());
                _inventory.keyCollected++;
            }
            else if(!key)
            {
                ChestComms _chestComms = GameObject.FindGameObjectWithTag("ChestParent").GetComponent<ChestComms>();
                _chestComms.AddWoodComms();
                _inventory.CollectWood(30);
            }

            
            
            
        }

        if (coll.gameObject)
        {
            print("Collision");
        }
    }
}
