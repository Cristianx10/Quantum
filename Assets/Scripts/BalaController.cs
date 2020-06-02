﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaController : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject obj;

    public bool left, right, up, down, ramdom = false;

    bool eliminando = false;

    Vector3 randomVector;
    // Start is called before the first frame update
    void Start()
    {
        rb.gravityScale = 0;
        randomVector = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f), 0);
        StartCoroutine(Reset());
    }

    // Update is called once per frame
    void Update()
    {
        if (up)
        {
            rb.AddForce(new Vector3(0, -10, 0), ForceMode2D.Impulse);
        }
        else if (down)
        {
            rb.AddForce(new Vector3(0, 10, 0), ForceMode2D.Impulse);
        }
        else if (left)
        {
            rb.AddForce(new Vector3(-10, 0, 0), ForceMode2D.Impulse);
        }
        else if (right)
        {
            rb.AddForce(new Vector3(10, 0, 0), ForceMode2D.Impulse);
        }
        else if (ramdom)
        {
            rb.AddForce(randomVector, ForceMode2D.Impulse);
        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            PlayerController player = col.gameObject.GetComponent<PlayerController>();
            if (player)
            {
                player.power -= 10;
            }
            Eliminar();
        }


    }


    void OnBecameInvisible()
    {
        Eliminar();

    }

    void Eliminar()
    {
        if (eliminando == false)
        {
            eliminando = true;
            Destroy(obj, 0);
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(5);
        Eliminar();
    }
}
