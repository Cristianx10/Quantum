using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoxCollider : MonoBehaviour
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
            player.colliderWall = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "ground")
        {
            StartCoroutine(Reset());
           
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(1);
        player.colliderWall = false;
    }
}
