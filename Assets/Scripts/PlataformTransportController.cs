using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformTransportController : MonoBehaviour
{

    public ArrayList players;
    public PlataformTransport from;
    public PlataformTransport to;

    void Start()
    {
        GameObject[] gameObjectsPlayer = GameObject.FindGameObjectsWithTag("Player");
        players = new ArrayList();
        foreach (var gameObjectPlayer in gameObjectsPlayer)
        {
            PlayerController player = gameObjectPlayer.GetComponent<PlayerController>();
            players.Add(player);
        }

    }

    public void AsignarPosiciones()
    {

        from.target = to;
        to.target = from;
    }

}
