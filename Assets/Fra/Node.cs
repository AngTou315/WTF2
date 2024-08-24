using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace HaoFsm
{
    public class Node
    {
        public FSMState state = FSMState.None;
        public Node nextNode;

        public virtual void OnEnter()
        {
            state = FSMState.Running;
        }
        public virtual void OnUpdate()
        {
        }
        public virtual void OnExit()
        {
            state = FSMState.End;
        }
        public Node Next()
        {
            OnExit();
            if (nextNode != null)
            {
                nextNode.OnEnter();
                return nextNode;
            }
            return null;
        }
    }
}

