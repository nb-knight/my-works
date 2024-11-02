using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ParticleSystemJobs;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    public InputController ic;
    public CapsuleCollider2D coll;
    public LayerMask targetLayerMask;
    public PhysicsCheck physicsCheck;
    public int i=1;
    public Vector2 InputDirection;
    public bool isHurt;
    public float hurtForce;
    public bool isDead;
    public bool isAttack;
    private PlayerAnimation PlayerAnimation;
    [Header("物理材质")]
    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D wall;
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        PlayerAnimation = GetComponent<PlayerAnimation>();
        ic.inputJson.Basic.attack.started += PlayerAttack;
        coll = GetComponent<CapsuleCollider2D>();
    }

   

    private void OnEnable() {
        ic.inputJson.Basic.Jump.performed += Jump;
        ic.inputJson.Basic.Interact.performed += Interact;
    }

    private void OnDisable() {
        ic.inputJson.Basic.Jump.performed -= Jump;
        ic.inputJson.Basic.Interact.performed -= Interact;
    }

    private void Interact(InputAction.CallbackContext context)
    {
        Collider2D[] results = new Collider2D[10];
        Physics2D.OverlapCircle(transform.position, 1.5f, new ContactFilter2D{useLayerMask=true, useTriggers=true, layerMask=targetLayerMask}, results);
        if(results[0]!=null) results[0].GetComponent<ShowContent>()?.ShowIt();
    }

    private void Jump(InputAction.CallbackContext context)
    {
        //i++;
        //if (physicsCheck.onGround)
          //  i = 0;
        //if (i < 2)
          //  rb.velocity = new Vector2(0,12);//直接赋值速度，防止下落与上升速度干扰！！！
          if(physicsCheck.onGround)
        {
            i = 1;
        }
        if (i >=0)
        {
            i--;
            rb.velocity = new Vector2(0, 12);
        }

    }

    private void PlayerAttack(InputAction.CallbackContext context)
    {
        PlayerAnimation.Playattack();
        isAttack = true;
    }

    private void Update() {

        if (!isHurt && !isAttack)
        {
            Vector2 dir = ic.inputJson.Basic.Move.ReadValue<Vector2>();
            rb.velocity = new Vector2(5 * dir.x, rb.velocity.y);
        }

        InputDirection = ic.inputJson.Basic.Move.ReadValue<Vector2>();
        int FaceDirection = (int)transform.localScale.x;
        if (InputDirection.x > 0)
            FaceDirection = 1;
        if (InputDirection.x < 0)
            FaceDirection = -1;
        transform.localScale = new Vector3(FaceDirection, 1, 1);
        CheckState();
    }

    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 0).normalized;
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
    }
    public void PlayerDead()
    {
        isDead = true;
        ic.inputJson.Basic.Disable();
    }
    private void CheckState()
    {
        coll.sharedMaterial = physicsCheck.onGround ? normal : wall;//一个简化的语句，：是否则就……
    }
}
