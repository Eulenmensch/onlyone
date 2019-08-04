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
    private float DefaultYPos;
    private LineRenderer Line;
    private BoxCollider2D BoxCollider;

    // Start is called before the first frame update
    void Start()
    {
        Line = GetComponent<LineRenderer>();
        BoxCollider = GetComponent<BoxCollider2D>();
        DefaultYPos = transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        MoveCollider();
        ResizeCollider();
        StretchAndSquash();
    }

    void StretchAndSquash()
    {
        Line.SetPosition(0, transform.position);
        Line.SetPosition(1, LineTarget.transform.position);
    }

    void ResizeCollider()
    {
        var midPos = transform.localPosition.y - 1;
        var offset = new Vector2(BoxCollider.offset.x, -midPos);
        BoxCollider.offset = offset;
        var size = new Vector2(BoxCollider.size.x, (transform.localPosition.y * 2) - 1);
        BoxCollider.size = size;
    }

    void MoveCollider()
    {
        if (DeformInput != 0)
        {
            if (DeformInput <= 0)
            {
                transform.DOLocalMoveY(SquashDepth, SquashTime);
            }
            if (DeformInput >= 0)
            {
                transform.DOLocalMoveY(StretchHeight, StretchTime);
            }
        }
        else
        {
            transform.DOLocalMoveY(DefaultYPos, SquashTime);
        }
    }

    void GetInput()
    {
        DeformInput = Input.GetAxisRaw("Vertical");
    }
}
