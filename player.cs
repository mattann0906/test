using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{

    public float speed;
    public float gravity;
    public float jumpSpeed;
    public float jumpHeight;

    public AnimationCurve dashCurve;
    public AnimationCurve jumpCurve;

    private Animator anim = null;
    private Rigidbody2D rb = null;
    private string groundTag = "Ground";
    private bool isGroundEnter, isGroundStay, isGroundExit;
    private bool isGround = false;
    private bool isJump = false;
    private float jumpPos = 0.0f;
    private float dashTime, jumpTime;
    private string dokanTag = "dokan";
    private bool isDokanEnter, isDokanStay, isDokanExit;
    private bool isDokan = false;
    private float beforeKey;
    private string goleTag = "gole";
    GameObject dokan;
    private string enemyTag = "Enemy";



    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();



    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (isGroundEnter || isGroundStay || isDokanEnter || isDokanStay)
        {
            isGround = true;
            isDokan = true;
        }
        else if (isGroundExit || isDokanExit)
        {
            isGround = false;
            isDokan = false;
        }

        float horizontalKey = Input.GetAxis("Horizontal");
        float verticalKey = Input.GetAxis("Vertical");

        float xSpeed = 0.0f;
        float ySpeed = -gravity;


        if (isGround)
        {
            if (verticalKey > 0)
            {
                ySpeed = jumpSpeed;
                jumpPos = transform.position.y;
                isJump = true;
            }
            else
            {
                isJump = false;
            }
        }
        else if (isJump)
        {
            if (verticalKey > 0 && jumpPos + jumpHeight > transform.position.y)
            {
                ySpeed = jumpSpeed;
                jumpTime += Time.deltaTime;
            }
            else
            {
                isJump = false;

                jumpTime = 0.0f;
            }
        }



        if (isDokan)
        {
           
            if (verticalKey > 0)
            {
                ySpeed = jumpSpeed;
                jumpPos = transform.position.y;
                isJump = true;
            }
            else
            {
                isJump = false;
            }
        }
        else if (isJump)
        {
            if (verticalKey > 0 && jumpPos + jumpHeight > transform.position.y)
            {
                ySpeed = jumpSpeed;
                jumpTime += Time.deltaTime;
            }
            else
            {
                isJump = false;

                jumpTime = 0.0f;
            }
        }





        if (isDokanEnter || isDokanStay)
        {
            if (verticalKey < 0)
            {

                Vector3 tmp1 = GameObject.Find("player_stand1").transform.position;
                Vector3 tmp2 = dokan.transform.Find("Deguti").position;
                GameObject.Find("player_stand1").transform.position = new Vector3(tmp1.x = tmp2.x, tmp1.y = tmp2.y, tmp1.z = tmp2.z);

            }
        }









        if (horizontalKey > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            anim.SetBool("run", true);
            xSpeed = speed;
            dashTime += Time.deltaTime;

        }
        else if (horizontalKey < 0)
        {

            transform.localScale = new Vector3(-1, 1, 1);
            anim.SetBool("run", true);
            xSpeed = -speed;
            dashTime += Time.deltaTime;
        }
        else
        {
            anim.SetBool("run", false);
            xSpeed = 0.0f;
            dashTime = 0.0f;
        }

        if (horizontalKey > 0 && beforeKey < 0)
        {
            dashTime = 0.0f;
        }
        else if (horizontalKey < 0 && beforeKey > 0)
        {
            dashTime = 0.0f;
        }

        xSpeed *= dashCurve.Evaluate(dashTime);
        if (isJump)
        {
            ySpeed *= jumpCurve.Evaluate(jumpTime);
        }

        rb.velocity = new Vector2(xSpeed, ySpeed);

        anim.SetBool("jump", isJump);
        anim.SetBool("ground", isGround);

        isGroundEnter = false;
        isGroundStay = false;
        isGroundExit = false;
        isDokanEnter = false;
        isDokanStay = false;
        isDokanExit = false;
        beforeKey = horizontalKey;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == groundTag))
        {
            isGroundEnter = true;
        }
        if (collision.tag == dokanTag)
        {
            isDokanEnter = true;
        }

        if (collision.tag == goleTag)
        {
            SceneManager.LoadScene("Game_Clear");
        }



    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        dokan = collision.transform.gameObject;


        if ((collision.tag == groundTag))
        {
            isGroundStay = true;
        }
        if (collision.tag == dokanTag)
        {
            isDokanStay = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.tag == groundTag))
        {
            isGroundExit = true;
        }
        if (collision.tag == dokanTag)
        {
            isDokanExit = true;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.collider.tag == enemyTag)
        //{
            Debug.Log("敵と接触しました");
        //}
    }
}

   


