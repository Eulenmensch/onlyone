using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    public float RayDistance;
    public float FlingHeight;
    public float FlingForce;
    public float Threshold;
    public GameObject Platform;

    private PlatformController PlatCont;
    private Rigidbody2D Rigidbody;
    private bool Airborne;

    // Start is called before the first frame update
    void Start()
    {
        PlatCont = Platform.GetComponent<PlatformController>();
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Fling();
        //SpringSwitch();
    }

    void Fling()
    {
        var isGrounded = GetGroundedOnPlatform();
        if (PlatCont.StretchHeight - Platform.transform.localScale.y <= Threshold && isGrounded)
        {
            print("fling!");
            Airborne = true;
            GetComponent<SpringJoint2D>().enabled = false;
            var xInput = Input.GetAxis("Horizontal");
            var flingDir = new Vector2(xInput, FlingHeight);
            Rigidbody.AddForce(flingDir * FlingForce);
        }
    }

    void SpringSwitch()
    {
        if (GetGroundedOnPlatform() && Airborne)
        {
            print("switch");
            GetComponent<SpringJoint2D>().enabled = true;
            Airborne = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == Platform)
        {
            print("collided");
            GetComponent<SpringJoint2D>().enabled = true;
            Airborne = false;
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
}
