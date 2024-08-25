using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HaoFsm
{
    public interface IFSM
    {
        FSMManager Manager { get; }
        void OnCreate(FSMManager manager);
        void OnEnter();
        void OnUpdate();
        void OnLateUpdate();
        void OnExit();
    }
    [Serializable]
    public abstract class FSM : IFSM
    {
        public FSMManager Manager { get; private set; }
        public virtual void OnCreate(FSMManager manager)
        {
            Manager = manager;
        }
        public abstract void OnEnter();

        public abstract void OnExit();

        public abstract void OnLateUpdate();

        public abstract void OnUpdate();
    }
}
