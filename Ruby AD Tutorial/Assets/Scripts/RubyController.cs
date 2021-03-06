﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public float speed = 3.0f;

    public int maxHealth = 5;

    public int health { get {return currentHealth; }}
    int currentHealth;

    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;

    Rigidbody2D rigidbody2D;
    // Start is called before the first frame update

    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    void Start()
    {
           rigidbody2D = GetComponent<Rigidbody2D>(); 

           currentHealth = maxHealth;

           animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.x);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);
           
        Vector2 position = rigidbody2D.position;

        position = position + move * speed * Time.deltaTime;

        rigidbody2D.MovePosition(position);

        if (isInvincible); {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            isInvincible = false;
        }

    }
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
            return;

            isInvincible =true;
            invincibleTimer = timeInvincible;

            animator.SetTrigger("Hit");
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }
}
