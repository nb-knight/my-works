using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, Interface//��������interface�е�һ������������ͬһ��������ͬ�����֣������ʵ���ý�chest��sorry
{
    public Vector3 positionToGo;
    public GameSceneSO sceneToGo;
    public SceneLoadEventSO eventSO;


    public void TriggerAction()
    {
        // Debug.Log("gogogo!!!");
        eventSO.RaiseloadRequestEvent(sceneToGo, positionToGo, true);//������֮ǰ�Ǹ���������
    }


}
