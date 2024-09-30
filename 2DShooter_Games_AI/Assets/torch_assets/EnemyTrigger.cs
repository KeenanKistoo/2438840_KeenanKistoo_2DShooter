using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Enemy")
        {
            trigger.gameObject.GetComponent<EnemyMovementData>()._enemyBehave = EnemyBehave.Freeze;
        }
    }
}
