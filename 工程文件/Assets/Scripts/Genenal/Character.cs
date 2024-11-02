using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//unity的事件，一次受伤做许多事
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("基本属性")]
    public float MaxHealth;
    public float CurrentHealth;
    [Header("受伤无敌")]
    public float wudiTime;
    public float wudiCounter;
    public bool wudi;

    public UnityEvent<Transform> OnTakeDamage;
    public UnityEvent Ondie;
    public UnityEvent<Character> OnHealthChange;//当血量变化时一个事件，触发这个事件，调用CharacterEventSO
    //然后出现在unity中，拖上OS，触发时广播OS
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
            //这里要执行受伤,执行OnTakeDamage这个事件
            OnTakeDamage?.Invoke(attacker.transform);
        }
        else
        {
            CurrentHealth = 0;
            //这里要触发死亡
            Ondie?.Invoke();
        }
        OnHealthChange?.Invoke(this);//广播用

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
