using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformTransportController : MonoBehaviour
{

    public PlayerController player;
    public PlataformTransport from;
    public PlataformTransport to;

    void Start()
    {
        GameObject gameObjectPlayer = GameObject.FindGameObjectWithTag("Player");
        player = gameObjectPlayer.GetComponent<PlayerController>();

    }

    public void AsignarPosiciones()
    {
    
        from.target = to;
        to.target = from;
    }

}
