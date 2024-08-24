using System.Collections;
using System.Collections.Generic;
using HaoFsm;
using UnityEngine;

public class NewBehaviourScript : FSM
{
      public override void OnEnter()
    {
        Debug.Log("NewBehaviourScriptOnEnter");
    }

    public override void OnExit()
    {
        Debug.Log("NewBehaviourScriptOnExit");
    }

    public override void OnLateUpdate()
    {
        Debug.Log("NewBehaviourScriptOnLateUpdate");
    }

    public override void OnUpdate()
    {
        Debug.Log("NewBehaviourScriptOnUpdate");
    }
}
