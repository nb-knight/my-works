using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sign : MonoBehaviour
{
    private Animator anim;//����һ������
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
        playerInput.Basic.comfirm.started += OnConfirm;//������ϵͳ�е�comfirmִ���ˣ�E��,�����������
    }

    private void OnConfirm(InputAction.CallbackContext context)
    {
        if (canPress)
        {
            targetItem.TriggerAction();//ֱ��ִ��Interface�еĺ���
        }
    }

    private void Update()
    {
        SignSprite.SetActive(canPress);//signsprite�Ƿ�������ͬ��canpress�ĶԹ��������Ǹ�С�������
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable")) //�Ӹ�tag�ж��㴳����������жϵ�2d��ײ���ǲ��ǿɻ����ġ�
        {
            canPress= true; 
            targetItem=collision.GetComponent<Interface>();//����ײʱ��öԷ����ϵĽӿ�
        }    
     }
    private void OnTriggerExit2D(Collider2D collision)
    {
        canPress = false;
    }

}
