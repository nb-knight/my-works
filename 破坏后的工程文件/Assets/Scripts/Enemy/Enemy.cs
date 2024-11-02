using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    protected Animator anim;//不写修饰符，因为public private之后可能会改，不加默认privatem，然后，用protected，仅允许子类访问
    PhysicsCheck physicsCheck;
    [Header("基本参数")]
    public float NormalSpeed;
    public float RunSpeed;
    public float CurrentSpeed;
    public Vector3 faceDir;
    [Header("计时器")]
    public float waittime;
    public float waittimeCounter;
    public bool wait;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();   
        anim = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();
        CurrentSpeed = NormalSpeed;
        waittimeCounter = waittime;
    }
    private void Update()
    {
        faceDir = new Vector3(-transform.localScale.x,0,0);//等于人物的实际面朝方向，加负号是因为猪是反的
        if((physicsCheck.NearRightWall&&faceDir.x>0)||(physicsCheck.NearLeftWall&&faceDir.x<0))
        {
            wait=true;
            anim.SetBool("walk", false);//这种远程调控动画的手段得熟悉，通过控制条件来
        }
        TimeCounter();
    }
    private void FixedUpdate()
    {
        
        Move();
    }
    public virtual void Move()//virtual——不是固定的，子类可以覆写
    {
        rb.velocity = new Vector2(CurrentSpeed * faceDir.x * Time.deltaTime, rb.velocity.y);
    }
    public void TimeCounter()
    {
        if (wait)
        {
            waittimeCounter -= Time.deltaTime;
            if (waittimeCounter <= 0)
            {
                wait = false;
                waittimeCounter = waittime;
                transform.localScale = new Vector3(faceDir.x, 1, 1);//正巧了脸朝向就是反的
            }
        }
    }
}
