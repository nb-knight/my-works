using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//unity���¼���һ�������������
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("��������")]
    public float MaxHealth;
    public float CurrentHealth;
    [Header("�����޵�")]
    public float wudiTime;
    public float wudiCounter;
    public bool wudi;

    public UnityEvent<Transform> OnTakeDamage;
    public UnityEvent Ondie;
    public UnityEvent<Character> OnHealthChange;//��Ѫ���仯ʱһ���¼�����������¼�������CharacterEventSO
    //Ȼ�������unity�У�����OS������ʱ�㲥OS
    private void Update()
    {
        if (wudi)
        {
            wudiCounter -= Time.deltaTime;
            if(wudiCounter<=0)
            {
                wudi = false;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("deadThing"))
        {
            CurrentHealth = 0;
            OnHealthChange?.Invoke(this);
            Ondie?.Invoke();
        }
    }
    private void Start()
    {
        CurrentHealth = MaxHealth;
        OnHealthChange?.Invoke(this);
    }

    public void TakeDamage(Attack attacker)
    {
        if (wudi)
            return;
        if (CurrentHealth - attacker.damage > 0)
        {
            CurrentHealth -= attacker.damage;
            TriggerWudi();
            //����Ҫִ������,ִ��OnTakeDamage����¼�
            OnTakeDamage?.Invoke(attacker.transform);
        }
        else
        {
            CurrentHealth = 0;
            //����Ҫ��������
            Ondie?.Invoke();
        }
        OnHealthChange?.Invoke(this);//�㲥��

    }
    public void TriggerWudi()
    {
        if(!wudi)
        {
            wudi = true;
            wudiCounter = wudiTime;
        }   
    }
   
}
