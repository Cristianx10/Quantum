using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargaGraphics : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject refGameObject;
     public GameObject refDesaparecer;

    public Material blue;
    public Material red;

    public ParticleSystem particulas;

    public int type = 0;

    public Renderer renderer;

    void Start()
    {
        renderer = refGameObject.GetComponent<Renderer>();
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


    public void starCorutina()
    {
        refDesaparecer.SetActive(false);
        StartCoroutine(Reset());
    }

    public IEnumerator Reset()
    {
        yield return new WaitForSeconds(5);
        refDesaparecer.SetActive(true);
    }
}
