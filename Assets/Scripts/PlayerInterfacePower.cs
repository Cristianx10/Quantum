using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterfacePower : MonoBehaviour
{
    PlayerController player;
    Material mPositivo;
    Material mNegativo;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
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
