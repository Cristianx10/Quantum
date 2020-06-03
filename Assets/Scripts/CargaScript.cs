using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargaScript : MonoBehaviour
{

    public GameObject refObject;
    public CargaGraphics carga;

    public BoxCollider2D box;


    // Start is called before the first frame update
    void Start()
    {
        carga = refObject.GetComponent<CargaGraphics>();
        // box = refObject.GetComponent<BoxCollider2D>();
    }



    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Player")
        {
            PlayerBoxCollider box = col.gameObject.GetComponent<PlayerBoxCollider>();
            if (carga.type == 0)
            {
                if (box.player.power < 0)
                {
                    box.player.power = Mathf.Abs(box.player.power) + 100;
                }
                else
                {
                    box.player.power += 100;
                }

            }
            else
            {
                if (box.player.power > 0)
                {
                    box.player.power = -Mathf.Abs(box.player.power) - 100;
                }
                else
                {
                    box.player.power -= 100;
                }
                //  box.player.power -= 100;
            }

            Debug.Log("Eliminar:  " + col.gameObject.tag);
            carga.starCorutina();
        }
      

    }

}
