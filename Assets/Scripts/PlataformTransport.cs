using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformTransport : MonoBehaviour
{

    private PlataformTransportController ptc;
    private ArrayList players;
    public Vector3 initPosition;
    public PlataformTransport target;

    public bool active = true;

    // Start is called before the first frame update
    void Start()
    {
        initPosition = transform.position;
        ptc = GetComponentInParent<PlataformTransportController>();
        players = ptc.players;

        if (ptc.from == null)
        {
            ptc.from = this;
        }
        else if (ptc.to == null)
        {
            ptc.to = this;
        }

        if (ptc.from && ptc.to)
        {
            ptc.AsignarPosiciones();
        }
    }

    // Update is called once per frame
    void Update()
    {



    }


    void OnCollisionEnter2D(Collision2D col)
    {



        if (active && col.gameObject.tag == "Player")
        {


            GameObject currentPlayer = col.gameObject;
            GameObject[] gameObjectsPlayers = GameObject.FindGameObjectsWithTag("Player");

            foreach (var gameObjectPlayer in gameObjectsPlayers)
            {
                GameObject playerG = gameObjectPlayer;
                if (playerG)
                {
                    playerG.transform.position = new Vector3(transform.position.x, transform.position.y + 1, playerG.transform.position.z);
                }
            }

            active = false;
            target.active = false;
            StartCoroutine(Reset());

        }




    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(2);
        active = true;
        target.active = true;
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (!active && col.gameObject.tag == "Player")
        {
            /*
            active = true;
            target.active = true;
            */
        }
    }
}
