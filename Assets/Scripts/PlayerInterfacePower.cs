using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterfacePower : MonoBehaviour
{
    PlayerController player;
    public Material mPositivo;
    public Material mNegativo;

    public GameObject medidor;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerController>();
      
    }

    // Update is called once per frame
    void Update()
    {
        if(player.power > 0){
           // gameObject.rende
        }

        float scalePower = map(player.power, -1, 1, -1, 1);
        transform.localScale = new Vector3(
            scalePower,
            transform.localScale.y,
            transform.localScale.z
            );
    }

    float map(float n, float start1, float stop1, float start2, float stop2)
    {
        return ((n - start1) / (stop1 - start1)) * (stop2 - start2) + start2;
    }




}
