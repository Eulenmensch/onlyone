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


    public float DoubleJumpForce;
    public float Speed;
    private Animator CharAnimator;
    private CharacterBehaviour CharBehaviour;


    // Start is called before the first frame update
    void Start()
    {
        PlatTopCont = Platform.GetComponent<PlatformTopController>();
        CharAnimator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Fling();
        Move();
        DoubleJump();
        FlipSprite();
    }

    void Fling()
    {
        var isGrounded = GetGroundedOnPlatform();
        if (PlatTopCont.StretchHeight - Platform.transform.localPosition.y <= Threshold && isGrounded)
        {
            print("fling!");
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
            return false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var contact = new ContactPoint2D();
        contact = other.GetContact(0);
        if (contact.normal == Vector2.up)
        {
            print("collided");
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
        }
        // else if (!Airborne)
        // {
        //     Rigidbody.velocity = new Vector2(0, Rigidbody.velocity.y);
        //     CharAnimator.SetBool("Walking", false);
        // }
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
