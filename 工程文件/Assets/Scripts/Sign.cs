using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sign : MonoBehaviour
{
    private Animator anim;//创建一个备用
    private InputJson playerInput;
    public GameObject SignSprite;
    private bool canPress;
    private Interface targetItem;
    private void Awake()
    {
        playerInput = new InputJson();
        playerInput.Enable();
    }
    private void OnEnable()
    {
        playerInput.Basic.comfirm.started += OnConfirm;//当输入系统中的comfirm执行了（E）,运行这个函数
    }

    private void OnConfirm(InputAction.CallbackContext context)
    {
        if (canPress)
        {
            targetItem.TriggerAction();//直接执行Interface中的函数
        }
    }

    private void Update()
    {
        SignSprite.SetActive(canPress);//signsprite是否启动，同步canpress的对钩与否到左边那个小框框里面
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable")) //加个tag判断你闯到的这个可判断的2d碰撞体是不是可互动的。
        {
            canPress= true; 
            targetItem=collision.GetComponent<Interface>();//在碰撞时获得对方身上的接口
        }    
     }
    private void OnTriggerExit2D(Collider2D collision)
    {
        canPress = false;
    }

}
