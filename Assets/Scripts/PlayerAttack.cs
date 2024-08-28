using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private float attackCd;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cdTimer = Mathf.Infinity;

    private void Awake(){
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update () {
        if (Input.GetMouseButton(0) && cdTimer > attackCd && playerMovement.canAttack()){
            Attack();
        }

        cdTimer += Time.deltaTime;
    }
    private void Attack(){
        anim.SetTrigger("attack");
        cdTimer = 0;
    }
}
