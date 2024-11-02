using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    private PlayerMove playerMove;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();
        rb = GetComponent<Rigidbody2D>();
        playerMove = GetComponent<PlayerMove>();
    }
    private void Update()
    {
        setAnimation();
    }
    public void setAnimation()
    {
        animator.SetFloat("velocityX",Mathf.Abs(rb.velocity.x));
        animator.SetFloat("velocityY", rb.velocity.y);
        animator.SetBool("IsGround", physicsCheck.onGround);
        animator.SetBool("isDead", playerMove.isDead);
        animator.SetBool("isAttack", playerMove.isAttack);
    }
    public void PlayerHurt()
    {
        animator.SetTrigger("hurt");
    }

    public void Playattack()
    {
        animator.SetTrigger("attack");
    }
}
