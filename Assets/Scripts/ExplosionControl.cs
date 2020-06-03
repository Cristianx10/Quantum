using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Reset());
    }


    IEnumerator Reset()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject, 0);
    }


}
