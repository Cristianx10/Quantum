using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathcolider : MonoBehaviour
{
    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Player")
        {
            PlayerController player = col.gameObject.GetComponent<PlayerController>();
            if (player)
            {
                player.power = 0;
            }
        }
    }
}
