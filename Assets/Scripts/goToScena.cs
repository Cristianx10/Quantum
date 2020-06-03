using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goToScena : MonoBehaviour
{

    public string scena;
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnCollisionEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(scena);

        }
    }

}
