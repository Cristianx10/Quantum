using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{

    private ArrayList players;

    public Rigidbody2D rb;
    public Animator anim;

    public bool isGrounded;
    public float speed = 24f;
    public float maxSpeed = 5f;
    public bool jump = false;
    public float jumpForce = 7f;

    public float gravedad = 9.8f;
    public int power = 100;

    public float orientacion = 1;
    public float orientacionX = 1;
    public float orientacionY = 1;

    bool moveLeft, moveRight = false;
    public float typePlayer = 0;

    public float attractiveForce = 20;
    public float attractiveDistance = 5;

    float temOrientacion = 0;

    public bool colliderWall = false;
    public bool touchPlayer = false;

    public float forceImpactA = 2;
    public float forceImpactB = 1.5f;

    public float minDistForce = 10;

    private float maxDistForce = 40;


    public float dis = 0;

    private bool isJumped = false;


    Vector3 vLeft = Vector2.left;
    Vector3 vRight = Vector2.right;
    Vector3 vUp = Vector2.up;
    Vector3 vDown = Vector2.down;


    UnityEngine.KeyCode up, down, left, right,
    initUp, initDown, initLeft, initRight, kdisparo;

    bool keySpace = false;

    private float timer = 0;
    private float temTimer = 0;

    public Transform prefactBala;

    public bool isDisparo = false;

    public int vista = 4;

    public int vistaUp = 1;
    public int vistaDown = 2;
    public int vistaLeft = 3;
    public int vistaRight = 4;


    public Light luz;

    private float minLuz = 0;
    private float maxLuz = 2;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponentInChildren<Animator>();

        players = new ArrayList();

        up = typePlayer == 0 ? KeyCode.UpArrow : KeyCode.W;
        down = typePlayer == 0 ? KeyCode.DownArrow : KeyCode.S;
        left = typePlayer == 0 ? KeyCode.LeftArrow : KeyCode.A;
        right = typePlayer == 0 ? KeyCode.RightArrow : KeyCode.D;
        kdisparo = typePlayer == 0 ? KeyCode.M : KeyCode.Q;

        initUp = up;
        initDown = down;
        initLeft = left;
        initRight = right;

    }

    // Update is called once per frame
    void Update()
    {

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

        if (power != 0)
        {
            timer -= (Time.deltaTime);

            int timeForm = (int)(timer);

            if (timeForm != temTimer)
            {
                temTimer = timeForm;


                if (power > 0)
                {
                    power -= 10;
                }
                else if (power < 0)
                {
                    power += 10;
                }


            }
        }

        if (Input.GetKeyDown(up) && isGrounded)
        {
            jump = true;
            isJumped = true;
        }


        if (Input.GetKeyDown(kdisparo))
        {

            Vector3 initVector = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            float dist = 2F;

            switch (vista)
            {
                case 1:
                    initVector = new Vector3(transform.position.x, transform.position.y - dist, transform.position.z);
                    break;
                case 2:
                    initVector = new Vector3(transform.position.x, transform.position.y + dist, transform.position.z);
                    break;
                case 3:
                    initVector = new Vector3(transform.position.x - dist, transform.position.y, transform.position.z);
                    break;
                case 4:
                    initVector = new Vector3(transform.position.x + dist, transform.position.y, transform.position.z);
                    break;
            }

            Transform balaTrasnform = Instantiate(prefactBala, initVector, Quaternion.identity);
            BalaController b = balaTrasnform.GetComponentInChildren<BalaController>();
            switch (vista)
            {
                case 1:
                    b.up = true;
                    break;
                case 2:
                    b.down = true;
                    break;
                case 3:
                    b.left = true;
                    break;
                case 4:
                    b.right = true;
                    break;
            }

            isDisparo = true;
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


        luz.intensity = map(Mathf.Abs(this.power), 0, 100, minLuz, maxLuz);
        luz.range = map(Mathf.Abs(this.power), 0, 100, 10, 200);

        // anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetBool("Grounded", isGrounded);
        anim.SetBool("Move", (moveLeft || moveRight));

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
            isJumped = false;
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


        if (Mathf.Abs(power) <= 11)
        {
            Eliminar();
        }


    }

    public void Eliminar()
    {
        Destroy(gameObject, 0);
    }

    void MagnetismoPlayers(bool changeOrientation)
    {

        for (int i = 0; i < players.Count; i++)
        {

            PlayerController player = (PlayerController)players[i];

            if (player != this)
            {

                Vector3 rel = player.transform.position;
                Vector3 pos = transform.position;

                Vector3 heading = rel - pos;

                float distance = heading.magnitude;
                Vector3 direction = player.transform.position - pos; // This is now the normalized direction.

                direction = direction.normalized;

                dis = distance;

                Vector3 dir1 = new Vector3(direction.x, direction.y, direction.z);
                Vector3 dir2 = new Vector3(-direction.x, -direction.y, direction.z);

                var cargasDiferentes = ((this.power > 10 && player.power < -10) || (player.power > 10 && power < -10));


                //cargasDiferentes = true;


                if (distance < attractiveDistance && (cargasDiferentes))
                {

                    if (!isChangeOrientationMove)
                    {

                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            keySpace = true;
                        }

                        if (colliderWall && player.isGrounded && !isJumped && !player.isJumped)
                        {
                            if (direction.y >= .9)
                            {
                                orientacion = 3;
                                isChangeOrientationMove = true;
                            }
                            else if (direction.y < -.9)
                            {
                                orientacion = 1;
                                if (temOrientacion == 1)
                                {
                                    temOrientacion = 0;
                                }
                                isChangeOrientationMove = true;
                            }
                        }

                        if (colliderWall && player.colliderWall)
                        {

                            if (direction.x >= .9)
                            {
                                orientacion = 4;
                                isChangeOrientationMove = true;
                            }
                            else if (direction.x < -.9)
                            {
                                orientacion = 2;
                                isChangeOrientationMove = true;
                            }

                        }


                        if (direction != null)
                        {

                            float temMaxDistForce = map(Mathf.Abs(power), 0, 100, 0, maxDistForce);
                            float fuerza = map(distance, 0, attractiveDistance, minDistForce, temMaxDistForce);

                            if (cargasDiferentes)
                            {
                                fuerza = map(distance, 0, attractiveDistance, minDistForce, maxDistForce);
                            }


                            rb.AddForce(dir1 * fuerza);

                        }
                        StartCoroutine(Reset());
                    }
                }
                else
                {
                    player.isChangeOrientationMove = false;
                    player.colliderWall = false;
                    player.orientacion = 1;

                }

                if (keySpace)
                {
                    keySpace = false;
                    // this.ImpactAtraction(dir2);
                    player.ImpactAtraction(dir1);


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

    void ImpactAtraction(Vector3 direction)
    {
        rb.AddForce((new Vector3(direction.x, direction.y, direction.z)) * jumpForce, ForceMode2D.Impulse);
        keySpace = false;
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

            vistaUp = 1;
            vistaDown = 2;
            vistaLeft = 3;
            vistaRight = 4;

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
            down = initLeft;
            left = initUp;
            right = initDown;

            vistaUp = 4;
            vistaDown = 3;
            vistaLeft = 1;
            vistaRight = 2;

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
            down = initUp;
            left = initRight;
            right = initLeft;

            vistaUp = 2;
            vistaDown = 1;
            vistaLeft = 4;
            vistaRight = 3;


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
            down = initRight;
            left = initDown;
            right = initUp;

            vistaUp = 3;
            vistaDown = 4;
            vistaLeft = 2;
            vistaRight = 1;

            /*transform.localScale = new Vector3(
                -1, transform.localScale.y, transform.localScale.z);*/
        }
    }

    void MoveHorizontalForce()
    {


        if (moveLeft)
        {
            vista = vistaLeft;
            transform.localScale = new Vector3(
                -1,
                transform.localScale.y,
                transform.localScale.z);

            rb.AddForce(vLeft * speed);
        }

        if (moveRight)
        {
            vista = vistaRight;
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


    float map(float n, float start1, float stop1, float start2, float stop2)
    {
        float value = ((n - start1) / (stop1 - start1)) * (stop2 - start2) + start2;
        if (value >= stop1)
        {
            value = stop1;
        }
        return value;
    }
}
