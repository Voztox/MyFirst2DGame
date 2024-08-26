using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float speed;

    private void Awake (){
        body = GetComponent<Rigidbody2D>();
    }

    private void Update(){
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

    }
}
