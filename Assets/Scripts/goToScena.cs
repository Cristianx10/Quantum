using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goToScena : MonoBehaviour
{

    public string scena;
    public int judadores = 1;
    private int dentro = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
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
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(scena);
    }

}
