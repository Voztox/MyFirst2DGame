using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCd;
    
    private void Awake()
    {   
        //Grabs references for rigidbody and animator from game object.
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
 
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
 
        //Flip player when facing left/right.
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);
 
        
 
        //sets animation parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());


        
        //wall jump cool down logic
        if(wallJumpCd > 0.2f)
        {
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        
            if(onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else 
                body.gravityScale = 7;
                
            if (Input.GetKey(KeyCode.W))
                Jump();
        }
        else
        wallJumpCd += Time.deltaTime;

    }
 
    private void Jump()
    {
        if(isGrounded()){
            
        body.velocity = new Vector2(body.velocity.x, jumpPower);
        anim.SetTrigger("jump");
        }
        else if (onWall() && !isGrounded()){
            wallJumpCd = 0;
            body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
         }
    }
 
    private void OnCollisionEnter2D(Collision2D collision)
    {
    }

    private bool isGrounded() {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    private bool onWall() {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
}