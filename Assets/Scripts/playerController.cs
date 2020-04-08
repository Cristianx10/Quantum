using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;

    public bool isGrounded;
    public float speed = 24f;
    public float maxSpeed = 6f;
    public bool jump = false;
    public float jumpForce = 7f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetBool("Grounded", isGrounded);

        if(Input.GetKeyDown(KeyCode.UpArrow) && isGrounded){
            jump = true;
        }

    }

    void FixedUpdate()
    {

        //Friccion

        if(isGrounded){
            Vector3 fixedVelocity = rb.velocity;
            fixedVelocity.x *= 0.9f;
            rb.velocity = fixedVelocity;
        }


        float h = Input.GetAxis("Horizontal");

        rb.AddForce(Vector2.right * speed * h);

        float limitedSpeed = Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed);
        rb.velocity = new Vector2(limitedSpeed, rb.velocity.y);

        if (h > 0.1f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if (h < -0.1f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if(jump){
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

  
}
