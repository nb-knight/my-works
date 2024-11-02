using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    protected Animator anim;//��д���η�����Ϊpublic private֮����ܻ�ģ�����Ĭ��privatem��Ȼ����protected���������������
    PhysicsCheck physicsCheck;
    [Header("��������")]
    public float NormalSpeed;
    public float RunSpeed;
    public float CurrentSpeed;
    public Vector3 faceDir;
    public float hurtForce;

    public Transform attacker;
    [Header("״̬")]
    public bool isAttack;
    public bool isDead;
    [Header("��ʱ��")]
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
        faceDir = new Vector3(-transform.localScale.x,0,0);//���������ʵ���泯���򣬼Ӹ�������Ϊ���Ƿ���
        if((physicsCheck.NearRightWall&&faceDir.x>0)||(physicsCheck.NearLeftWall&&faceDir.x<0))
        {
            wait=true;
            anim.SetBool("walk", false);//����Զ�̵��ض������ֶε���Ϥ��ͨ������������
        }
        TimeCounter();
    }
    private void FixedUpdate()
    {
        if(!isAttack&&!isDead) 
        Move();
    }
    public virtual void Move()//virtual�������ǹ̶��ģ�������Ը�д
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
                transform.localScale = new Vector3(faceDir.x, 1, 1);//��������������Ƿ���
            }
        }
    }
    public void OnTakeDamage(Transform attackerTransform)
    {
        attacker= attackerTransform;
        if ((attackerTransform.position.x - transform.position.x )> 0)//���������Ҳ�,��ȥ���ǹ��Լ�������
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        //������else��ͬʱ�����жϣ���Ȼ����ɵ���������ʲô��
        if ((attackerTransform.position.x-transform.position.x )< 0)
        {
            transform.localScale=new Vector3(1,1,1);
        }
        //���˼���
        isAttack= true;
        anim.SetTrigger("hurt");
        Vector2 dir=new Vector2(attackerTransform.position.x-transform.position.x,0).normalized;//normalized��׼��������һ���������ʱ��������
        //���˺󣬵õ����򣬿�ʼЯ�̺���
        StartCoroutine(OnHurt(dir));
    }
    private IEnumerator OnHurt(Vector2 dir)//voidû�з���ֵ���������һ��������
        //dir�Ǻ�������ʱ���������Ե��ȴ��ݽ���
    {
        rb.AddForce(-dir * hurtForce, ForceMode2D.Impulse);//����ִ����֮���ٱ��false
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
