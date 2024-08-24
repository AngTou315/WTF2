using System.Collections;
using System.Collections.Generic;
using HaoFsm;
using UnityEngine;

public class Test : FSM
{
    public override void OnEnter()
    {
        Debug.Log("OnEnter");
    }

    public override void OnExit()
    {
        Debug.Log("OnExit");
    }

    public override void OnLateUpdate()
    {
        Debug.Log("OnLateUpdate");
    }

    public override void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            FSMManager.Next();
        }
        Debug.Log("OnUpdate");
    }
}
