//有广播就得有接听，这个manager就是接听Character广播的
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerSetBar playerSetBar;
    [Header("事件监听")]
    public CharacterEventSO HealthEvent;

    private void OnEnable()
    {
        //注册事件
        HealthEvent.OnEventRaised += OnHealthEvent;
        //调用CharacterEventSO,里面那个事件，要监听的那个事件
        //+=注册函数事件，来相应你的这次监听
            }

    private void OnDisable()
    {
        //取消注册
        HealthEvent.OnEventRaised -= OnHealthEvent;
    }

    private void OnHealthEvent(Character character)//这里记得改名，不然下面写Character引用的不知道什么东西
    {
        //这个函数的作用，我猜是修改ui血量显示
        var persentage = character.CurrentHealth / character.MaxHealth;
       playerSetBar.OnHealthChange(persentage);

    }
}
