using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformTransport : MonoBehaviour
{

    private PlataformTransportController ptc;
    private PlayerController player;
    public Vector3 initPosition;
    public PlataformTransport target;

    public bool active = true;

    // Start is called before the first frame update
    void Start()
    {
        initPosition = transform.position;
        ptc = GetComponentInParent<PlataformTransportController>();
        player = ptc.player;

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
            active = false;
            target.active = false;
            player.transform.position = target.transform.position;
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
