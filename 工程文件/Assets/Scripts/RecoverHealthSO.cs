using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(menuName = "Event/RecoverHealthSO")]
public class RecoverHealthSO : ScriptableObject
{
    public UnityAction<Character> HealthHealth;
    public void RaiseEvent(Character character)
    {
        HealthHealth?.Invoke(character);
    }
}
