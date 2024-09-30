using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Button Inputs:")]
    private float _hInput;
    private float _vInput;
    private float _speed = 3f;
    private bool _isFacingRight;

    public bool playerReady;

    [Header("GameObject's Rigibody:")]
    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        playerReady = false;
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        _hInput = Input.GetAxisRaw("Horizontal");
        _vInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            playerReady = true;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(_hInput * _speed, _vInput * _speed);
    }

    private void Move()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

   
}
