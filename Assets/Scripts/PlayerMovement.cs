using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float speed;
    private Animator anime;

    //Grab referencer from the objects.
    private void Awake (){
        body = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
    }

    private void Update(){
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

        //flip player for left and right turns.
        if(horizontalInput > 0.01f){
            transform.localScale = Vector3.one;
        }
        else if(horizontalInput < -0.01f){
            transform.localScale = new Vector3(-1,1,1);
        }

        if(Input.GetKey(KeyCode.W)){
            body.velocity = new Vector2(body.velocity.x, speed/2);
        }
        //animator parameters
        anime.SetBool("run", horizontalInput != 0); 
    }
}
