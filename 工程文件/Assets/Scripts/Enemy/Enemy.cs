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
    public float hurtForce;

    public Transform attacker;
    [Header("状态")]
    public bool isAttack;
    public bool isDead;
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
        if(!isAttack&&!isDead) 
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
    public void OnTakeDamage(Transform attackerTransform)
    {
        attacker= attackerTransform;
        if ((attackerTransform.position.x - transform.position.x )> 0)//攻击者在右侧,减去的是怪自己的坐标
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        //不能用else，同时两层判断，不然容易傻，先左后右什么的
        if ((attackerTransform.position.x-transform.position.x )< 0)
        {
            transform.localScale=new Vector3(1,1,1);
        }
        //受伤鸡腿
        isAttack= true;
        anim.SetTrigger("hurt");
        Vector2 dir=new Vector2(attackerTransform.position.x-transform.position.x,0).normalized;//normalized标准化，正负一。这段是临时变量储存
        //受伤后，得到方向，开始携程函数
        StartCoroutine(OnHurt(dir));
    }
    private IEnumerator OnHurt(Vector2 dir)//void没有返回值，这个返回一个迭代器
        //dir是函数内临时变量，所以得先传递进来
    {
        rb.AddForce(-dir * hurtForce, ForceMode2D.Impulse);//让他执行完之后，再变成false
        yield return new WaitForSeconds(0.65f);
        isAttack = false;
    }
    public void OnDie()
    {
        gameObject.layer = 2;
        anim.SetBool("dead", true);
        isDead = true;
    }
    public void OnDestroy()
    {
        Destroy(this.gameObject);
    }
}
