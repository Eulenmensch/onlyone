using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    public float RayDistance;
    public float FlingHeight;
    public float FlingForce;
    public float Threshold;
    public float DoubleJumpForce;
    public GameObject Platform;

    private PlatformTopController PlatTopCont;
    private Rigidbody2D Rigidbody;
    private bool Airborne;
    private bool DoubleJumped;

    // Start is called before the first frame update
    void Start()
    {
        PlatTopCont = Platform.GetComponent<PlatformTopController>();
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Fling();
        DoubleJump();
        //SpringSwitch();
    }

    void Fling()
    {
        var isGrounded = GetGroundedOnPlatform();
        if (PlatTopCont.StretchHeight - Platform.transform.position.y <= Threshold && isGrounded)
        {
            print("fling!");
            Airborne = true;
            GetComponent<SpringJoint2D>().enabled = false;
            var xInput = Input.GetAxis("Horizontal");
            var flingDir = new Vector2(xInput, FlingHeight);
            Rigidbody.AddForce(flingDir * FlingForce);
        }
    }

    void DoubleJump()
    {
        if (Input.GetButtonDown("Jump") && Airborne & !DoubleJumped)
        {
            Rigidbody.AddForce(Vector2.up * DoubleJumpForce, ForceMode2D.Impulse);
            DoubleJumped = true;
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
        if (other.gameObject == Platform)
        {
            print("collided");
            GetComponent<SpringJoint2D>().enabled = true;
            Airborne = false;
            DoubleJumped = false;
        }
    }
}
