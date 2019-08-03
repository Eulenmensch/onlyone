using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlatformTopController : MonoBehaviour
{
    public float SquashDepth;
    public float SquashTime;
    public float StretchHeight;
    public float StretchTime;
    public GameObject LineTarget;

    private float DeformInput;
    private float DefaultXPos;
    private LineRenderer Line;

    // Start is called before the first frame update
    void Start()
    {
        Line = GetComponent<LineRenderer>();
        DefaultXPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        MoveCollider();
        StretchAndSquash();
    }

    void StretchAndSquash()
    {
        Line.SetPosition(0, transform.position);
        Line.SetPosition(1, LineTarget.transform.position);
    }

    void MoveCollider()
    {
        if (DeformInput != 0)
        {
            if (DeformInput <= 0)
            {
                transform.DOMoveY(SquashDepth, SquashTime);
            }
            if (DeformInput >= 0)
            {
                transform.DOMoveY(StretchHeight, StretchTime);
            }
        }
        else
        {
            transform.DOMoveY(DefaultXPos, SquashTime);
        }
    }

    void GetInput()
    {
        DeformInput = Input.GetAxisRaw("Vertical");
    }
}
