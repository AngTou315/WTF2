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
            var ob = Manager.GetData("456");
            Transform trans = ob as Transform;
            Debug.Log(trans.name);
           
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            // 下一步
            Manager.Next();
        }
        Debug.Log("OnUpdate");
    }
}
