using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goToScena : MonoBehaviour
{

    public string scen = "";
    public int judadores = 1;
    public int dentro = 0;
    // Start is called before the first frame update

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Playerc")
        {
            PlayerController player = col.gameObject.GetComponent<PlayerController>();
            player.gano = true;
            dentro += 1;
            if (dentro >= judadores)
            {
                StartCoroutine(GO());
            }

        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            PlayerController player = col.gameObject.GetComponent<PlayerController>();
            player.gano = false;

            dentro -= 1;
        }
    }




    IEnumerator GO()
    {
        yield return new WaitForSeconds(1);
        Debug.Log(scen);

        GameObject[] gameObjectsPlayers = GameObject.FindGameObjectsWithTag("Player");

        foreach (var gameObjectPlayer in gameObjectsPlayers)
        {
            PlayerController player = gameObjectPlayer.GetComponent<PlayerController>();
            if (player)
            {
                player.perdio = true;
            }
        }

        SceneManager.LoadScene(scen);
    }

}
