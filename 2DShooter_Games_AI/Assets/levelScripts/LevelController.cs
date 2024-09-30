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

    private void Start()
    {
        mainPanel.SetActive(true);
        tutPanel.SetActive(false);
        loadPane.SetActive(false);
    }

    public void SetLevel(int val)
    {
        _gen.corridorCount = val;
        _gen.corridorLength = (val - 5);
        StartCoroutine(StallForGen());
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
