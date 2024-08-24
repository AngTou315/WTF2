using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HaoFsm
{
    public interface IFSM
    {
        void OnEnter();
        void OnUpdate();
        void OnLateUpdate();
        void OnExit();
    }
    [Serializable]
    public abstract class FSM : IFSM
    {
        public abstract void OnEnter();

        public abstract void OnExit();

        public abstract void OnLateUpdate();

        public abstract void OnUpdate();
    }
}
