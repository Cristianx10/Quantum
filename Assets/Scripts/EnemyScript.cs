using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    public Transform balaPrefact;
    private ArrayList players;
    // Start is called before the first frame update

    public float vida = 3;


    private float timer = 0;
    private float temTimer = 0;

    private bool eliminando = false;

    int timeForm = 0;

    void Start()
    {
        players = new ArrayList();

        GameObject[] gameObjectsPlayers = GameObject.FindGameObjectsWithTag("Player");

        foreach (var gameObjectPlayer in gameObjectsPlayers)
        {
            PlayerController player = gameObjectPlayer.GetComponent<PlayerController>();
            players.Add(player);
        }




    }

    // Update is called once per frame
    void Update()
    {
        timer -= (Time.deltaTime);
        timeForm = (int)(timer);


        //Muere el personaje
        if (vida <= 0)
        {
            Destroy(gameObject, 0);
        }

    }

    void FixedUpdate()
    {
        ataque();
    }



    void ataque()
    {

        if (timeForm != temTimer)
        {
            for (int i = 0; i < players.Count; i++)
            {

                PlayerController player = (PlayerController)players[i];

                if (player)
                {
                    Vector3 rel = player.transform.position;
                    Vector3 pos = transform.position;

                    Vector3 heading = rel - pos;

                    float distance = heading.magnitude;
                    Vector3 direction = player.transform.position - pos; // This is now the normalized direction.

                    direction = direction.normalized;



                    temTimer = timeForm;
                    Vector3 initVector = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                    Transform balaTrasnform = Instantiate(balaPrefact, initVector, Quaternion.identity);
                    BalaController b = balaTrasnform.GetComponentInChildren<BalaController>();
                    b.refVector = direction;
                    b.isRefVector = true;
                }


            }
        }
    }
}
