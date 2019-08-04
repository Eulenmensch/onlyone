using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlatformController : MonoBehaviour
{
    public float Speed;

    Rigidbody2D Rigidbody;
    Animator PlatAnimator;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        PlatAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MovementInputKeyboard();
        FlipSprite();
    }

    void MovementInputKeyboard()
    {
        var xInput = Input.GetAxisRaw("PlatformHorizontal");
        if (xInput != 0)
        {
            var xVel = xInput * Speed * Time.deltaTime;
            Rigidbody.velocity = new Vector2(xVel, Rigidbody.velocity.y);
            PlatAnimator.SetBool("Walking", true);
        }
        else
        {
            Rigidbody.velocity = new Vector2(0, Rigidbody.velocity.y);
            PlatAnimator.SetBool("Walking", false);
        }
    }

    void Jump()
    {

    }

    void FlipSprite()
    {
        var xInput = Input.GetAxisRaw("PlatformHorizontal");
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
