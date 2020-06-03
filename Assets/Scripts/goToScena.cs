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
            dentro += 1;

            if (dentro >= judadores)
            {
                SceneManager.LoadScene(scena);
            }

        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            dentro -= 1;
        }
    }

}
