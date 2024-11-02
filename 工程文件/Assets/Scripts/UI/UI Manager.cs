//�й㲥�͵��н��������manager���ǽ���Character�㲥��
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerSetBar playerSetBar;
    [Header("�¼�����")]
    public CharacterEventSO HealthEvent;

    private void OnEnable()
    {
        //ע���¼�
        HealthEvent.OnEventRaised += OnHealthEvent;
        //����CharacterEventSO,�����Ǹ��¼���Ҫ�������Ǹ��¼�
        //+=ע�ắ���¼�������Ӧ�����μ���
            }

    private void OnDisable()
    {
        //ȡ��ע��
        HealthEvent.OnEventRaised -= OnHealthEvent;
    }

    private void OnHealthEvent(Character character)//����ǵø�������Ȼ����дCharacter���õĲ�֪��ʲô����
    {
        //������������ã��Ҳ����޸�uiѪ����ʾ
        var persentage = character.CurrentHealth / character.MaxHealth;
       playerSetBar.OnHealthChange(persentage);

    }
}
