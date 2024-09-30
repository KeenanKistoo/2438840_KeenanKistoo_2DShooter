using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public CorridorFirstDungeonGenerator _gen;

    public GameObject mainPanel;
    public GameObject tutPanel;
    public GameObject loadPane;

    [SerializeField]
    private Loadbar _loadbar;

    [SerializeField] private ItemDistribution _item;

    private void Start()
    {
        
        mainPanel.SetActive(true);
        _item = GameObject.FindGameObjectWithTag("ItemController").GetComponent<ItemDistribution>();
        tutPanel.SetActive(false);
        loadPane.SetActive(false);
    }

    public void SetLevel(int val)
    {
        _gen.corridorCount = val;
        _gen.corridorLength = (val - 5);
        StartCoroutine(StallForGen());
    }

    public void SetChest(int val)
    {
        _item.desChest = val;
    }

    public void SetKey(int val)
    {
        Inventory _inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();

        _inventory.keyLvl = val;
    }

    public void SetEnemy(int val)
    {
        _item.desEnemy = val;
    }

    public void SetTorch(int val)
    {
        _item.desTorch = val;

    }

    private IEnumerator StallForGen()
    {
        
        loadPane.SetActive(true);
        _loadbar.StartLoad(10f);
        _gen.GenerateDungeon();
        yield return new WaitForSeconds(10f);
        
        //tutPanel.SetActive(true);
        loadPane.SetActive(false);
        mainPanel.SetActive(false);
        
    }
}
