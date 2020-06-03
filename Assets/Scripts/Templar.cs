using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Templar : MonoBehaviour
{
    // Start is called before the first frame update

    Quaternion temRot;
    public bool temblar = false;

    private int numRamdon = 0;

    void Start()
    {
        temRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (temblar)
        {
            numRamdon++;
            numRamdon = (int)(map(Mathf.Cos(numRamdon), 0, 1, -20, 20));
            Quaternion rand = new Quaternion(numRamdon, 0, 0, 0);
            transform.rotation = rand;
        }
        else
        {
            transform.rotation = temRot;
        }
    }

     float map(float n, float start1, float stop1, float start2, float stop2)
    {
        float value = ((n - start1) / (stop1 - start1)) * (stop2 - start2) + start2;
        if (value >= stop2)
        {
            value = stop2;
        }
        return value;
    }
}
