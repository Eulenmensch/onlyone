using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouseBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var pos = Input.mousePosition;
        pos.z = 10;
        pos = Camera.main.ScreenToWorldPoint(pos);
        transform.position = pos;
    }
}
