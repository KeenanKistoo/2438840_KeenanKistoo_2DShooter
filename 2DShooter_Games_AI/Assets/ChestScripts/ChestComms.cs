using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestComms : MonoBehaviour
{
    public GameObject addWood;
    public GameObject subWood;

    private void Start()
    {
        addWood.SetActive(false);
        subWood.SetActive(false);
    }

    public void AddWoodComms()
    {
        StartCoroutine(Add());
    }

    private IEnumerator Add()
    {
        addWood.SetActive(true);

        yield return new WaitForSeconds(2f);
        
        addWood.SetActive(false);
    }

    public void SubWoodComms()
    {
        StartCoroutine(Sub());
    }

    private IEnumerator Sub()
    {
        subWood.SetActive(true);

        yield return new WaitForSeconds(2f);
        
        subWood.SetActive(false);
    }
}
