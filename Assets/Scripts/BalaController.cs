using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaController : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject obj;

    public bool left, right, up, down = false;

    // Start is called before the first frame update
    void Start()
    {
        rb.gravityScale = 0;
        
     
    }

    // Update is called once per frame
    void Update()
    {
        if (up)
        {
            rb.AddForce(new Vector3(0, -10, 0), ForceMode2D.Impulse);
        }
        else if (down)
        {
            rb.AddForce(new Vector3(0, 10, 0), ForceMode2D.Impulse);
        }
        else if (left)
        {
            rb.AddForce(new Vector3(-10, 0, 0), ForceMode2D.Impulse);
        }
        else if (right)
        {
            rb.AddForce(new Vector3(10, 0, 0), ForceMode2D.Impulse);
        }

    }

    void OnBecameInvisible()
    {

        Destroy(obj, 0);
    }
}
