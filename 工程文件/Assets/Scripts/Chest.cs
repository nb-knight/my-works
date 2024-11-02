using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, Interface//下面获得了interface中的一个函数方法，同一个方法不同的名字，这个其实不该叫chest，sorry
{
    public Vector3 positionToGo;
    public void TriggerAction()
    {
        Debug.Log("gogogo!!!");
    }

 
}
