using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Templar : MonoBehaviour
{
    // Start is called before the first frame update

    Vector3 temPos;
    public bool temblar = false;
    void Start()
    {
        temPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (temblar)
        {
            Vector3 randomVector = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f), 0);
            transform.position = randomVector;
        }
        else
        {
            transform.position = temPos;
        }
    }
}
