using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    public Vector2 bottomOffset;
    public Vector2 leftOffset;
    public Vector2 rightOffset;
    public bool onGround;
    public bool NearLeftWall;
    public bool NearRightWall;
    public LayerMask groundlayer;
    public float checkRaduis;
    private void Update()
    {
        Check();
    }
    private void Check()
    {
        NearLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRaduis, groundlayer);
        NearRightWall = Physics2D.OverlapCircle((Vector2)transform.position+rightOffset,checkRaduis, groundlayer);
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRaduis,groundlayer);
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset,checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, checkRaduis);
    }
}
