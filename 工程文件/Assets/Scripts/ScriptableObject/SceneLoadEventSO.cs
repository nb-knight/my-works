using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName ="Event/SceneLoadEventSO")]
public class SceneLoadEventSO : ScriptableObject
{
    public UnityAction<GameSceneSO,Vector3,bool> loadRequestEvent;
    public void RaiseloadRequestEvent(GameSceneSO locationToLoad,Vector3 positionToGo,bool fadeScreen)
    {
        loadRequestEvent?.Invoke(locationToLoad, positionToGo, fadeScreen);//要的场景，player目的地，是否需要淡入淡出特效
    }
}
