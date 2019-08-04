// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CharacterMovementController : MonoBehaviour
// {
//     public float DoubleJumpForce;
//     public float Speed;

//     private Rigidbody2D Rigidbody;
//     private Animator CharAnimator;
//     private CharacterBehaviour CharBehaviour;
//     private bool Airborne;
//     private bool DoubleJumped;

//     // Start is called before the first frame update
//     void Start()
//     {
//         Rigidbody = GetComponent<Rigidbody2D>();
//         CharAnimator = GetComponent<Animator>();
//         CharBehaviour = GetComponent<CharacterBehaviour>();
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         Airborne = CharBehaviour.Airborne;
//         DoubleJumped = CharBehaviour.DoubleJumped;
//         Move();
//         DoubleJump();
//         FlipSprite();
//     }

//     void Move()
//     {
//         var xInput = Input.GetAxisRaw("CharacterHorizontal");
//         if (xInput != 0)
//         {
//             var xVel = xInput * Speed * Time.deltaTime;
//             Rigidbody.velocity = new Vector2(xVel, Rigidbody.velocity.y);
//             CharAnimator.SetBool("Walking", true);
//         }
//         // else if (!Airborne)
//         // {
//         //     Rigidbody.velocity = new Vector2(0, Rigidbody.velocity.y);
//         //     CharAnimator.SetBool("Walking", false);
//         // }
//         else
//         {
//             CharAnimator.SetBool("Walking", false);
//         }
//     }

//     void DoubleJump()
//     {
//         if (Input.GetButtonDown("Jump") & !DoubleJumped)
//         {
//             Rigidbody.AddForce(Vector2.up * DoubleJumpForce, ForceMode2D.Impulse);
//             DoubleJumped = true;
//         }
//     }

//     void FlipSprite()
//     {
//         var xInput = Input.GetAxisRaw("CharacterHorizontal");
//         if (xInput < 0)
//         {
//             transform.localScale = new Vector2(-1, 1);
//         }
//         if (xInput > 0)
//         {
//             transform.localScale = new Vector2(1, 1);
//         }
//     }
// }
