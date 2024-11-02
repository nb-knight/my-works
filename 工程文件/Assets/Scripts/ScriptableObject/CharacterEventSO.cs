using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName="Event/CharacterEventSO")]
public class CharacterEventSO : ScriptableObject
{
    public UnityAction<Character> OnEventRaised;//传出去Character
    public void RaiseEvent(Character character)//谁想要用这个Raise……函数（即想要传出去），就把自己的character传进去
    {
        OnEventRaised?.Invoke(character);//?是看看你有没有订阅这个事件，invoke是启动这个事件，启动后把Character传进去
    }
    //事件的订阅与调用都卸载了一起
}
