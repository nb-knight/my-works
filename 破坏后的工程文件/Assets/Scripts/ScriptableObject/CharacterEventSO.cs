using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName="Event/CharacterEventSO")]
public class CharacterEventSO : ScriptableObject
{
    public UnityAction<Character> OnEventRaised;//����ȥCharacter
    public void RaiseEvent(Character character)//˭��Ҫ�����Raise��������������Ҫ����ȥ�����Ͱ��Լ���character����ȥ
    {
        OnEventRaised?.Invoke(character);//?�ǿ�������û�ж�������¼���invoke����������¼����������Character����ȥ
    }
    //�¼��Ķ�������ö�ж����һ��
}
