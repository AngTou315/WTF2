using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HaoFsm
{
    public enum FSMState
    {
        None, // 未开始
        Running, // 正在运行
        End, // 结束运行
    }
    public class StateNode<T> : Node where T : IFSM
    {
        private T fsm;
        public StateNode(T fsm)
        {
            this.fsm = fsm;
        }
        public override void OnEnter()
        {
            fsm.OnEnter();
            base.OnEnter();
        }
        public override void OnUpdate()
        {
            fsm.OnUpdate();
            base.OnUpdate();
            fsm.OnLateUpdate();
        }
        public override void OnExit()
        {
            fsm.OnExit();
            base.OnExit();
        }
        // public override Node Next()
        // {
        //     OnExit();
        //     if (nextNode != null)
        //     {
        //         nextNode.OnEnter();
        //         return nextNode;
        //     }
        //     return null;
        // }

    }
}

