using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargaGraphics : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject refGameObject;

    public Material blue;
    public Material red;

    public ParticleSystem particulas;

    public int type = 0;

    Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (type == 0)
        {
            renderer.material = blue;
            particulas.startColor = Color.blue;
        }
        else
        {
            renderer.material = red;
        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Player")
        {
            PlayerController player = col.gameObject.GetComponent<PlayerController>();
            if (type == 0)
            {
                player.power += 100;
            }
            else
            {
                player.power -= 100;
            }

            Debug.Log("Eliminar");
            refGameObject.SetActive(false);
            StartCoroutine(Reset());
        }
    }

     IEnumerator Reset()
    {
        yield return new WaitForSeconds(5);
        refGameObject.SetActive(true);
    }
}
