﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGrounded : MonoBehaviour
{

    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerController>();
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "ground")
        {
            player.isGrounded = true;
            player.jump = false;
        }
        else if (col.gameObject.tag == "Player")
        {
            player.isGrounded = true;
            player.jump = false;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "ground")
        {
            player.isGrounded = false;
        }
        else if (col.gameObject.tag == "Player")
        {
            player.isGrounded = false;
        }
    }


    void OnBecameInvisible()
    {
        player.transform.position = new Vector3(0, 0, 0);
    }
}
