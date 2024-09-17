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

    private void FlashlightRotation()
    {
        // Step 1: Get the mouse position in world space
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Step 2: Set the Z position to match the object's Z position (usually 0 in 2D)
        mousePos.z = transform.position.z;

        // Step 3: Calculate the direction vector from the flashlight object to the mouse
        Vector3 direction = mousePos - transform.position;

        // Step 4: Calculate the angle in degrees for 2D rotation
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Step 5: Apply the rotation to the flashlight object (only affecting the Z axis)
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 75);
        
    }
}