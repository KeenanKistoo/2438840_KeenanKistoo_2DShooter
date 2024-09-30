using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TorchLight : MonoBehaviour
{
   [SerializeField] private bool lit;

   [SerializeField] private GameObject _messagePanel;

   [SerializeField] private GameObject _player;

   [SerializeField] private bool collision;

    
   [SerializeField] private Sprite litFire1;
   [SerializeField] private Sprite litFire2;
   [SerializeField] private Sprite unlitFire;

   [SerializeField] private GameObject _torch;

   private void Awake()
   {
      _messagePanel = GameObject.FindGameObjectWithTag("MessagePanel");
      _player = GameObject.FindWithTag("Player");
      collision = false;
      _torch.SetActive(false);
   }

   private void Update()
   {
      if (collision && Input.GetKey(KeyCode.Q))
      {
         Inventory _inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
         
         //print("Working");
         if (_inventory.woodCount >= 10)
         {
            this.gameObject.GetComponent<AudioSource>().Play();
            StartCoroutine(LightFire());
         }
         else if (_inventory.woodCount < 10)
         {
            StartCoroutine(_inventory.NotEnough());
         }
            
      }
   }

   private void OnTriggerEnter2D(Collider2D coll)
   {
      if (coll.gameObject == _player)
      {
         _messagePanel.SetActive(true);
         collision = true;
      }
   }

   private void OnTriggerExit2D(Collider2D coll)
   {
      if (coll.gameObject == _player)
      {
         _messagePanel.SetActive(false);
         collision = false;
      }
   }

   private IEnumerator LightFire()
   {
      SpriteRenderer _spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
      _spriteRenderer.sprite = litFire1;
      collision = false;
      _messagePanel.SetActive(false);
      _torch.SetActive(true);
      ChestComms _chestComms = GameObject.FindGameObjectWithTag("ChestParent").GetComponent<ChestComms>();
      Inventory _inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
      
      _chestComms.SubWoodComms();
      _inventory.UseWood(10);
      
      yield return new WaitForSeconds(0.5f);
      _spriteRenderer.sprite = litFire2;
      
      yield return new WaitForSeconds(0.5f);
      _spriteRenderer.sprite = litFire1;
      
      yield return new WaitForSeconds(0.5f);
      _spriteRenderer.sprite = litFire2;
      
      yield return new WaitForSeconds(0.5f);
      _spriteRenderer.sprite = litFire1;
      
      yield return new WaitForSeconds(0.5f);
      _spriteRenderer.sprite = litFire2;
      
      yield return new WaitForSeconds(0.5f);
      _spriteRenderer.sprite = litFire1;
      
      yield return new WaitForSeconds(0.5f);
      _spriteRenderer.sprite = unlitFire;
      _torch.SetActive(false);

      
      
   } 

}
