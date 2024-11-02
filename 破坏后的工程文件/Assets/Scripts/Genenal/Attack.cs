using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;
    public float attackRange;
    public float attackRate;
    private void OnTriggerStay2D(Collider2D other)
    {
       other.GetComponent<Character>()?.TakeDamage(this);//加个？，使他判断碰撞物有无这个代码
    }

}
