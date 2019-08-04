using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    public float RayDistance;
    public float FlingHeight;
    public float FlingForce;
    public float Threshold;
    private bool Airborne;
    private bool DoubleJumped;
    public GameObject Platform;

    private PlatformTopController PlatTopCont;
    private Rigidbody2D Rigidbody;
    private float Timer;
    public float FlingTimer;
    public bool TimerStarted;


    public float DoubleJumpForce;
    public float Speed;
    private Animator CharAnimator;
    private CharacterBehaviour CharBehaviour;
    private SpringJoint2D Spring;


    // Start is called before the first frame update
    void Start()
    {
        Platform = GameObject.FindGameObjectWithTag("TopPlatform");
        PlatTopCont = Platform.GetComponent<PlatformTopController>();
        CharAnimator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Spring = GetComponent<SpringJoint2D>();
        Spring.connectedBody = Platform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        FlingInput();
        Move();
        DoubleJump();
        FlipSprite();
        GetGroundedOnPlatform();
    }

    void FlingInput()
    {
        if (Input.GetAxisRaw("Vertical") != 0 && !TimerStarted)
        {
            TimerStarted = true;
        }
        if (TimerStarted)
        {
            Timer += Time.deltaTime;
        }
        if (Input.GetAxisRaw("Vertical") == 0 && Timer <= FlingTimer && TimerStarted)
        {
            Fling();
            Timer = 0;
            TimerStarted = false;
        }
        else if (Input.GetAxisRaw("Vertical") == 0 && Timer > FlingTimer)
        {
            Timer = 0;
            TimerStarted = false;
        }
    }

    void Fling()
    {
        var isGrounded = GetGroundedOnPlatform();
        if (isGrounded)
        {
            Airborne = true;
            CharAnimator.SetBool("Airborne", true);
            GetComponent<SpringJoint2D>().enabled = false;
            var xInput = Input.GetAxis("Horizontal");
            var flingDir = new Vector2(xInput, FlingHeight);
            Rigidbody.AddForce(flingDir * FlingForce);
        }
    }

    bool GetGroundedOnPlatform()
    {
        var ray = Physics2D.Raycast(transform.position, Vector2.down, RayDistance);
        if (ray.transform == Platform.transform)
        {
            return true;
        }
        else
        {
            Spring.enabled = false;
            return false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var contact = new ContactPoint2D();
        contact = other.GetContact(0);
        if (contact.normal == Vector2.up)
        {
            Airborne = false;
            CharAnimator.SetBool("Airborne", false);
            DoubleJumped = false;
            if (other.gameObject == Platform)
            {
                GetComponent<SpringJoint2D>().enabled = true;
            }
        }
    }

    void Move()
    {
        var xInput = Input.GetAxisRaw("CharacterHorizontal");
        if (xInput != 0)
        {
            var xVel = xInput * Speed * Time.deltaTime;
            Rigidbody.velocity = new Vector2(xVel, Rigidbody.velocity.y);
            CharAnimator.SetBool("Walking", true);
            Spring.enabled = false;
        }
        else if (!Airborne)
        {
            Rigidbody.velocity = new Vector2(0, Rigidbody.velocity.y);
            CharAnimator.SetBool("Walking", false);
            Spring.enabled = true;
        }
        else
        {
            CharAnimator.SetBool("Walking", false);
        }
    }

    void DoubleJump()
    {
        if (Input.GetButtonDown("Jump") & !DoubleJumped)
        {
            Rigidbody.AddForce(Vector2.up * DoubleJumpForce, ForceMode2D.Impulse);
            DoubleJumped = true;
        }
    }

    void FlipSprite()
    {
        var xInput = Input.GetAxisRaw("CharacterHorizontal");
        if (xInput < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        if (xInput > 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
    }
}
