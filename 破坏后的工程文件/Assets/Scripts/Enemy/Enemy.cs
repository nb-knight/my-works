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
}
