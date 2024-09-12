using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightBehaviour : MonoBehaviour
{
    private void Update()
    {
        FlashlightRotation();
    }
    
    public void FlashlightRotation()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        Vector3 _direction = mousePos - transform.position;

        float _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        
        transform.rotation = Quaternion.Euler(0f,0f, _angle);
    }
}
