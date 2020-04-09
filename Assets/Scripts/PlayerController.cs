﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;

    public bool isGrounded;
    public float speed = 24f;
    public float maxSpeed = 6f;
    public bool jump = false;
    public float jumpForce = 7f;

    public float gravedad = 9.8f;

    public float orientacionY = 1;

    bool moveLeft, moveRight = false;

    public float typePlayer = 0;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetBool("Grounded", isGrounded);

        var up = typePlayer == 0 ? KeyCode.UpArrow : KeyCode.W;


        if (Input.GetKeyDown(up) && isGrounded)
        {
            jump = true;
        }

        var left = typePlayer == 0 ? KeyCode.LeftArrow : KeyCode.A;
        var right = typePlayer == 0 ? KeyCode.RightArrow : KeyCode.D;


        if (Input.GetKeyDown(left))
        {
            moveLeft = true;
        }

        if (Input.GetKeyDown(right))
        {
            moveRight = true;
        }


        if (Input.GetKeyUp(left))
        {
            moveLeft = false;
        }

        if (Input.GetKeyUp(right))
        {
            moveRight = false;
        }




    }

    void typeMovePlayer(bool value, bool keyUp)
    {



    }

    void FixedUpdate()
    {

        //Gravedad
        rb.AddForce(Vector2.down * gravedad * orientacionY);

        //Friccion

        if (isGrounded)
        {
            Vector3 fixedVelocity = rb.velocity;
            fixedVelocity.x *= 0.9f;
            rb.velocity = fixedVelocity;
        }




        if (moveLeft || moveRight)
        {
            float h = Input.GetAxis("Horizontal");

            rb.AddForce(Vector2.right * speed * h);
            float limitedSpeed = Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed);
            rb.velocity = new Vector2(limitedSpeed, rb.velocity.y);

            if (h > 0.1f)
            {
                transform.localScale = new Vector3(1f, transform.localScale.y, 1f);
            }

            if (h < -0.1f)
            {
                transform.localScale = new Vector3(-1f, transform.localScale.y, 1f);
            }

        }



        if (orientacionY == -1)
        {
            transform.localScale = new Vector3(transform.localScale.x, -1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, 1f, 1f);
        }



        if (jump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce * orientacionY, ForceMode2D.Impulse);
            jump = false;
        }


    }


}
