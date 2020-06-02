using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargaScript : MonoBehaviour
{

    public GameObject refObject;
    public CargaGraphics carga;
   

    // Start is called before the first frame update
    void Start()
    {
        carga = refObject.GetComponent<CargaGraphics>();

    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Player")
        {
            PlayerController player = col.gameObject.GetComponent<PlayerController>();
            if (carga.type == 0)
            {
                player.power += 100;
            }
            else
            {
                player.power -= 100;
            }

            Debug.Log("Eliminar");
            carga.starCorutina();
        }
    }

    void FixedUpdate()
    {

    }




}
