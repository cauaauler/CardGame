// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PlayerMovement : MonoBehaviour
// {
//     public float moveSpeed;
//     private Rigidbody2D rb;
//     private Vector2 moveDirection;

//     void Start()
//     {
//         rb = GetComponent<Rigidbody2D>();
//     }
//     void Inputs(){
//         float moveX = Input.GetAxisRaw("Horizontal");
//         float moveY = Input.GetAxisRaw("Vertical");

//         moveDirection = new Vector2(moveX, moveY);
//     }
//     void Move(){
//         rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed) /** Time.deltaTime*/;
//     }
//     void Update(){
//         Inputs();
//     }
//     void FixedUpdate() { 
//         Move();
//     }
// }
