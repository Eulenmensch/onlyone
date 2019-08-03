using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlatformController : MonoBehaviour
{
    public float Speed;
    public float CrouchDepth;
    public float CrouchTime;
    public float StretchHeight;
    public float StretchTime;

    Rigidbody2D Rigidbody;
    Vector3 StartScale;
    Animator PlatAnimator;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        PlatAnimator = GetComponent<Animator>();
        StartScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        MovementInputKeyboard();
        //StretchSquash();
        FlipSprite();
    }

    void MovementInputKeyboard()
    {
        var xInput = Input.GetAxisRaw("Horizontal");
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

    void FlipSprite()
    {
        var xInput = Input.GetAxisRaw("Horizontal");
        if (xInput < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        if (xInput > 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
    }

    void StretchSquash()
    {
        var yInput = Input.GetAxisRaw("Vertical");
        if (yInput != 0)
        {
            if (yInput <= 0)
            {
                transform.DOScaleY(CrouchDepth, CrouchTime);
            }
            if (yInput >= 0)
            {
                transform.DOScaleY(StretchHeight, StretchTime);
            }
        }
        else
        {
            transform.DOScale(StartScale, CrouchTime);
        }
    }
}
