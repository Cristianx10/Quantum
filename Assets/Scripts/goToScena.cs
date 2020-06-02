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

    void OnCollisionEnter2D()
    {
        SceneManager.LoadScene(scena);
    }

}
