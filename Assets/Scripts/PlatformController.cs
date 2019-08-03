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

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        StartScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        MovementInputKeyboard();
    }

    void MovementInputKeyboard()
    {
        var xInput = Input.GetAxisRaw("Horizontal");
        var yInput = Input.GetAxisRaw("Vertical");
        if (xInput != 0)
        {
            var xVel = xInput * Speed * Time.deltaTime;
            Rigidbody.velocity = new Vector2(xVel, Rigidbody.velocity.y);
        }
        else
        {
            Rigidbody.velocity = new Vector2(0, Rigidbody.velocity.y);
        }
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
