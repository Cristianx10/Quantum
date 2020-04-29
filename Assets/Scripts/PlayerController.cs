using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class PlayerController : MonoBehaviourPun
{

    private ArrayList players;
    public Rigidbody2D rb;
    private Animator anim;

    public bool isGrounded;
    public float speed = 24f;
    public float maxSpeed = 5f;
    public bool jump = false;
    float jumpForce = 7f;

    public float gravedad = 9.8f;
    public float power = 1;

    public float orientacion = 1;
    public float orientacionX = 1;
    public float orientacionY = 1;

    bool moveLeft, moveRight = false;
    public float typePlayer = 0;

    float attractiveForce = 10;
    public float attractiveDistance = 3;

    float temOrientacion = 0;

    public bool colliderWall = false;
    public bool touchPlayer = false;


    Vector3 vLeft = Vector2.left;
    Vector3 vRight = Vector2.right;
    Vector3 vUp = Vector2.up;
    Vector3 vDown = Vector2.down;


    UnityEngine.KeyCode up, down, left, right,
    initUp, initDown, initLeft, initRight;






    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        players = new ArrayList();
        GameObject[] gameObjectsPlayers = GameObject.FindGameObjectsWithTag("Player");
        foreach (var gameObjectPlayer in gameObjectsPlayers)
        {
            PlayerController player = gameObjectPlayer.GetComponent<PlayerController>();
            players.Add(player);
        }

        up = typePlayer == 0 ? KeyCode.UpArrow : KeyCode.W;
        down = typePlayer == 0 ? KeyCode.DownArrow : KeyCode.S;
        left = typePlayer == 0 ? KeyCode.LeftArrow : KeyCode.A;
        right = typePlayer == 0 ? KeyCode.RightArrow : KeyCode.D;

        initUp = up;
        initDown = down;
        initLeft = left;
        initRight = right;

    }

    // Update is called once per frame
    void Update()
    {

        
            anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
            anim.SetBool("Grounded", isGrounded);
            anim.SetBool("Move", (moveLeft || moveRight));


        if (photonView.IsMine)
        {

            if (Input.GetKeyDown(up) && isGrounded)
            {
                jump = true;
            }

            if (Input.GetKeyDown(left))
            {
                moveLeft = true;
            }

            if (Input.GetKeyDown(right))
            {
                moveRight = true;
            }


            if (Input.GetKeyUp(left))
            {
                moveLeft = false;
            }

            if (Input.GetKeyUp(right))
            {
                moveRight = false;
            }
        }
    }

    void FixedUpdate()
    {

        ConfigOrientation();

        //Gravedad

        rb.AddForce(vDown * gravedad);

        //Friccion

        if (isGrounded)
        {
            float mxspeed = 0.9f;
            Vector3 fixedVelocity = rb.velocity;
            if (orientacion == 1 || orientacion == 3)
            {
                fixedVelocity.x *= mxspeed;
            }
            else if (orientacion == 2 || orientacion == 4)
            {
                fixedVelocity.y *= mxspeed;
            }
            rb.velocity = fixedVelocity;
        }


        MoveHorizontalForce();
        // MoveHorizontal();

        MagnetismoPlayers(false);


        if (jump)
        {
            //rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(vUp * jumpForce, ForceMode2D.Impulse);
            jump = false;
        }

    }



    void MagnetismoPlayers(bool changeOrientation)
    {
        foreach (var playerObject in players)
        {
            PlayerController player = (PlayerController)playerObject;
            if (player != this)
            {
                var maxRange = attractiveDistance;

                Vector3 rel = player.transform.position;
                Vector3 pos = transform.position;

                Vector3 heading = rel - pos;

                var distance = heading.magnitude;
                var direction = heading / distance; // This is now the normalized direction.

                if (!isChangeOrientationMove)
                {


                    if (heading.sqrMagnitude < maxRange * maxRange)
                    {
                        //  orientacionY *= -1;

                        if (!touchPlayer)
                        {


                            if (colliderWall && !player.colliderWall && !isGrounded && player.isGrounded)
                            {
                                if (direction.y >= .9)
                                {
                                    orientacion = 3; isChangeOrientationMove = true;
                                }
                                else if (direction.y < -.9)
                                {
                                    orientacion = 1; isChangeOrientationMove = true;
                                }
                            }

                            if (colliderWall && player.colliderWall)
                            {

                                if (direction.x >= .9)
                                {
                                    orientacion = 4; isChangeOrientationMove = true;
                                }
                                else if (direction.x < -.9)
                                {
                                    orientacion = 2; isChangeOrientationMove = true;
                                }

                            }
                        }
                        else
                        {
                            orientacion = 1;
                        }

                        if (direction != null)
                        {
                            rb.AddForce(direction * attractiveForce);
                        }


                        StartCoroutine(Reset());
                    }
                    else
                    {
                        isChangeOrientationMove = false;
                        colliderWall = false;
                        orientacion = 1;
                    }
                }

            }

            if (temOrientacion != orientacion)
            {
                temOrientacion = orientacion;
                rb.velocity = new Vector3(0, 0, 0);
                moveLeft = false;
                moveRight = false;
                jump = false;
            }

        }
    }

    bool isChangeOrientationMove = false;

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(2);
        isChangeOrientationMove = false;
    }


    void ConfigOrientation()
    {
        if (orientacion == 1)
        {
            vUp = Vector2.up;
            vRight = Vector2.right;
            vDown = Vector2.down;
            vLeft = Vector2.left;
            transform.rotation = Quaternion.Euler(0, 0, 0);

            up = initUp;
            down = initDown;
            left = initLeft;
            right = initRight;

            /*transform.localScale = new Vector3(
                transform.localScale.x, 1, transform.localScale.z);*/
        }
        else if (orientacion == 2)
        {
            vUp = Vector2.right;
            vRight = Vector2.down;
            vDown = Vector2.left;
            vLeft = Vector2.up;
            transform.rotation = Quaternion.Euler(0, 0, 270);

            up = initRight;
            right = initDown;
            down = initLeft;
            left = initUp;

            /*transform.localScale = new Vector3(
                1, transform.localScale.y, transform.localScale.z);*/
        }
        else if (orientacion == 3)
        {
            vUp = Vector2.down;
            vRight = Vector2.left;
            vDown = Vector2.up;
            vLeft = Vector2.right;
            transform.rotation = Quaternion.Euler(0, 0, 180);


            up = initDown;
            right = initLeft;
            down = initUp;
            left = initRight;

            /*transform.localScale = new Vector3(
                transform.localScale.x, -1, transform.localScale.z);*/
        }
        else if (orientacion == 4)
        {
            vUp = Vector2.left;
            vRight = Vector2.up;
            vDown = Vector2.right;
            vLeft = Vector2.down;
            transform.rotation = Quaternion.Euler(0, 0, 90);

            up = initLeft;
            right = initUp;
            down = initRight;
            left = initDown;

            /*transform.localScale = new Vector3(
                -1, transform.localScale.y, transform.localScale.z);*/
        }
    }

    void MoveHorizontalForce()
    {


        if (moveLeft)
        {
            transform.localScale = new Vector3(
                -1,
                transform.localScale.y,
                transform.localScale.z);

            rb.AddForce(vLeft * speed);
        }

        if (moveRight)
        {
            transform.localScale = new Vector3(
                1,
                transform.localScale.y,
                transform.localScale.z);

            rb.AddForce(vRight * speed);
        }

        if (orientacion == 1 || orientacion == 3)
        {
            float limitedSpeed = Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed);
            rb.velocity = new Vector2(limitedSpeed, rb.velocity.y);
        }
        else if (orientacion == 2 || orientacion == 4)
        {
            float limitedSpeed = Mathf.Clamp(rb.velocity.y, -maxSpeed, maxSpeed);
            rb.velocity = new Vector2(rb.velocity.x, limitedSpeed);
        }



    }

    void MoveHorizontal()
    {
        if (moveLeft || moveRight)
        {
            float h = Input.GetAxis("Horizontal");

            /*
                        rb.AddForce(Vector2.right * speed * h);
                        float limitedSpeed = Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed);
                        rb.velocity = new Vector2(limitedSpeed, rb.velocity.y);
            */
            if (h > 0.1f)
            {
                transform.localScale = new Vector3(1f, transform.localScale.y, 1f);
            }

            if (h < -0.1f)
            {
                transform.localScale = new Vector3(-1f, transform.localScale.y, 1f);
            }

        }
    }


}
