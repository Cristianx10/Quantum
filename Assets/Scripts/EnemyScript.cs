using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    public Transform balaPrefact;
    private ArrayList players;
    // Start is called before the first frame update


    private float timer = 0;
    private float temTimer = 0;

    private bool eliminando = false;

    void Start()
    {
        players = new ArrayList();

    }

    // Update is called once per frame
    void Update()
    {


        timer -= (Time.deltaTime);

        int timeForm = (int)(timer);

        if (timeForm != temTimer)
        {
            temTimer = timeForm;


            Vector3 initVector = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Transform balaTrasnform = Instantiate(balaPrefact, initVector, Quaternion.identity);
            BalaController b = balaTrasnform.GetComponentInChildren<BalaController>();
            b.ramdom = true;


        }




        if (players.Count < 2)
        {
            GameObject[] gameObjectsPlayers = GameObject.FindGameObjectsWithTag("Player");

            if (gameObjectsPlayers.Length >= 2)
            {

                foreach (var gameObjectPlayer in gameObjectsPlayers)
                {
                    PlayerController player = gameObjectPlayer.GetComponent<PlayerController>();
                    players.Add(player);

                }

            }
        }

        
    }
}
