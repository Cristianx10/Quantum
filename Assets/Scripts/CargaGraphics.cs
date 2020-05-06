using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargaGraphics : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject gameObject;

    public int type = 0;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Player")
        {
            PlayerController player = col.gameObject.GetComponent<PlayerController>();
            player.power = 2;

            Debug.Log("Eliminar");
            Destroy(gameObject, 0f);
        }

    }
}
